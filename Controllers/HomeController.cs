using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogH.Controllers
{
    using Models;
    using App_Classes;

    public class HomeController : Controller
    {
        // GET: Home
        Data context = new Data();
        public ActionResult Index()
        {

            return View();
        }
       public ActionResult MakaleListele()
        {
            var data = context.Makale.ToList();
            return View("MakaleListeleWidget",data);
        }
        public PartialViewResult PopulerMakalelerWidget()
        {
            //OrderByDescending son eklenenleri en başta gösterir.
            var model = context.Makale.OrderByDescending(x => x.EklenmeTarihi).Take(3).ToList();
            return PartialView(model);
        }
    }
}