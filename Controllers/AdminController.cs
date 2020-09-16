using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogH.Controllers
{
    using Models;
    public class AdminController : Controller
    {
        // GET:


        Data context = new Data();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YazarOnayları()
        {
            var data = context.Kullanici.Where(x => x.Yazar == true && x.Onaylandi == false).ToList();
            return View(data);
        }
        public ActionResult OnayVer(int id)
        {
            Kullanici kl = context.Kullanici.FirstOrDefault(x => x.KullaniciId == id);
            kl.Onaylandi = true;
            context.SaveChanges();
            return RedirectToAction("YazarOnaylari");

        }
    }
}