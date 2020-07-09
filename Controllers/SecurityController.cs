using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCdeneme2.Models.EntityFramework;

namespace MVCdeneme2.Controllers
{
    public class SecurityController : Controller
    {
        OkulDbEntities db = new OkulDbEntities();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var user_ = db.User.FirstOrDefault(x => x.Ad==user.Ad && x.Sifre == user.Sifre);
            if (user_ != null)
            {
                FormsAuthentication.SetAuthCookie(user_.Ad, false);
                return RedirectToAction("Index", "OgrenciDers");
            }
            else
            {
                ViewBag.Message = "Kullanıcı adı veya şifre yanlış.";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Ogrenci");
        }
    }
}