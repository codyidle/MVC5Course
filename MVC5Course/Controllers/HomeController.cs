using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HandleError(ExceptionType = typeof(InvalidOperationException),View ="Error2")]
        public ActionResult About(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("沒有帶入差數");

            throw new InvalidOperationException("其他錯誤");

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}