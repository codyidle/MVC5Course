using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC5Course.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel data)
        {
            if (CheckLogin(data))
            {
                FormsAuthentication.RedirectFromLoginPage(data.Email, false);
                return RedirectToAction("Index", "Home");
            }
            return View(data);
        }

        private bool CheckLogin(LoginViewModel data)
        {
            return (data.Email == "cody@edetw.com" && data.Password == "123");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Register()
        {
            return View();
        }
        [Authorize]
        public ActionResult EditProfile()
        {
            return View();
        }
    }
}