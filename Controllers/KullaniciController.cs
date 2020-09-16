using BlogH.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogH.Controllers
{
    public class KullaniciController : Controller
    {
        // GET: Kullanici
        Data context = new Data();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GirisYap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(Kullanici kl)
        {
            string ka = ValidateUser(kl.KullaniciAdi, kl.Parola);
           
            if (!string.IsNullOrEmpty(ka))
            {
                //Role göre kullanıcının giriş yapmasını sağlar süresini vs ayarlayabiliriz.
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,kl.KullaniciAdi,DateTime.Now,DateTime.Now.AddMinutes(15),true,ka,FormsAuthentication.FormsCookiePath);

                HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName);

                if (ticket.IsPersistent)
                {
                    ck.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(ck);
                FormsAuthentication.RedirectFromLoginPage(kl.KullaniciAdi, true);

                return RedirectToAction("Index", "Home");
            }
          
            return RedirectToAction("GirisYap");
        }
        string ValidateUser(string ka, string pwd)
        {
            Kullanici kl = context.Kullanici.FirstOrDefault(x => x.KullaniciAdi == ka && x.Parola == pwd);
            if (kl!=null)
            {
                return kl.KullaniciAdi;

            }
            else
            {
                return "";
            }
           

        }
        public ActionResult CikisYap(string redirectUrl)
        {
            FormsAuthentication.SignOut();
            return Redirect(redirectUrl);
        }
        public ActionResult UyeOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeOl(Kullanici kl,string rdBay, string rdBayan)
        {
            if (!string.IsNullOrEmpty(rdBay))
            {
                kl.Cinsiyet = true;
                if (!string.IsNullOrEmpty(rdBayan))
                {
                    kl.Cinsiyet = false;
                }
                kl.Yazar = false;
                kl.Onaylandi = true;
                kl.Aktif = true;
                kl.DogumTarihi = kl.DogumTarihi.Value.Date;
                kl.KayitTarihi = DateTime.Now;
                context.Kullanici.Add(kl);
                context.SaveChanges();
             

            }
            return RedirectToAction("GirisYap");
        }
    }
}