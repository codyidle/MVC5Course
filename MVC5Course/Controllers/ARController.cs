using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return PartialView("Index");
        }

        public ActionResult FileTest(int? dl)
        {
            if (dl.HasValue && dl == 1)
                return File(Server.MapPath("~/mario.png"), "image/png", "download.png");
            else
                return File(Server.MapPath("~/mario.png"), "image/png");
        }

        public ActionResult JsonTest(int id)
        {
            var db = repoProduct.UnitOfWork.Context;

            db.Configuration.LazyLoadingEnabled = false;

            var product = repoProduct.Find(id);
            return Json(product,JsonRequestBehavior.AllowGet);
        }
    }
}