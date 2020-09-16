using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogH.Controllers
{
    using Models;
    public class EtiketController : Controller
    {
        Data context = new Data();
        // GET: Etiket
        public ActionResult Index(int id)
        {
            return View(id);
        }
        public PartialViewResult EtiketlerWidget()
        {
            return PartialView(context.Etiket.ToList());
        }
    
    public ActionResult MakaleListele(int id)
        {
            var data = context.Makale.Where(x => x.Etiket.Any(y => y.EtiketId == id)).ToList();
            return View("MakaleListeleWidget",data);
        }
    }
}