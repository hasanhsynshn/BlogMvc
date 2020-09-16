using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogH.Controllers
{
    using BlogH.App_Classes;
    using Models;
    using System.Drawing;

    [Authorize]
    public class MakaleController : Controller
    {
        Data context = new Data();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Detay(int id)
        {
            var data = context.Makale.FirstOrDefault(x => x.MakaleId == id);
            return View(data);


        }
        [Authorize(Roles ="Admin")]
        public ActionResult MakaleEkle()
        {
            return View();
        }
        [AllowAnonymous]
        public string Begen(int id)
        {
            Makale mkl = context.Makale.FirstOrDefault(x => x.MakaleId == id);
            mkl.Begeni++;
            context.SaveChanges();
            return mkl.Begeni.ToString();
        }
        [Authorize(Roles ="Admin,Yazar")]
        public ActionResult Ekle()
        {
            ViewBag.Kategoriler = context.Kategori.ToList();

            return View();

        }
        [Authorize(Roles = "Admin,Yazar")]

        [HttpPost]
        public ActionResult Ekle(Makale mkl, HttpPostedFileBase resim)
        {
            Image img = Image.FromStream(resim.InputStream);
            Bitmap kckResim = new Bitmap(img, Settings.ResimKucukBoyut);
            Bitmap ortaResim = new Bitmap(img, Settings.ResimOrtaBoyut);
            Bitmap buyukResim = new Bitmap(img, Settings.ResimBuyukBoyut);
            kckResim.Save(Server.MapPath("/Content/MakaleResim/KucukBoyut/" + resim.FileName));
            ortaResim.Save(Server.MapPath("/Content/MakaleResim/OrtaBoyut/" + resim.FileName));
            buyukResim.Save(Server.MapPath("/Content/MakaleResim/BuyukBoyut/" + resim.FileName));
            Resim rsm = new Resim();
            rsm.BuyukBoyut = "/Content/MakaleResim/BuyukBoyut/" + resim.FileName;
            rsm.KucukBoyut = "/Content/MakaleResim/KucukBoyut/" + resim.FileName;
            rsm.OrtaBoyut = "/Content/MakaleResim/OrtaBoyut/" + resim.FileName;
            context.Resim.Add(rsm);
            context.SaveChanges();
            mkl.ResimID = rsm.ResimId;
            mkl.EklenmeTarihi = DateTime.Now;
            mkl.GoruntulenmeSayisi = 0;
            mkl.Begeni = 0;
            int yzrId = context.Kullanici.FirstOrDefault(x => x.KullaniciAdi == User.Identity.Name).KullaniciId;
            mkl.YazarID = yzrId;
            context.Makale.Add(mkl);
            context.SaveChanges();
            return RedirectToAction("Home", "Index");

              

        }
    }
}