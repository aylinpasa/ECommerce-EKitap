using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using System.Data;

namespace ECommerce.Controllers
{
    public class LoginController : Controller
    {
        ECommerce_TESTEntities db = new ECommerce_TESTEntities();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserProfile objUser)
        {
            SessionUserInfo.User = null;
            if (ModelState.IsValid)
            {
                using (ECommerce_TESTEntities db = new ECommerce_TESTEntities())
                {
                    var obj = db.UserProfile.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        SessionUserInfo.User = obj;
                        return RedirectToAction("Home");
                    }
                }
            }
            return View("Index");
        }

        public ActionResult Home()
        {
            if (SessionUserInfo.User != null)
            {
                if (SessionUserInfo.User.UserTypeId != 1)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Index", "Product");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
             SessionUserInfo.User = null;
            
                return RedirectToAction("Login");
            
        }

      
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(SessionUserInfo model)
        {
            if (model.UserId <= 0)
            {
                db.UserProfile.Add(new UserProfile
                {
                    Name = model.FirsName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName,
                    Email = model.Email,
                });
            }
            db.SaveChanges();

            return View("Index");
        }

    }
}