using GlobalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlobalChat.Controllers
{
    public class LogInController : Controller
    {
        [HttpGet]
        public ActionResult Authorize()
        {
            return View(new LogInModel());
        }

        [HttpPost]
        public ActionResult Authorize(LogInModel logInModel)
        {
            using (var dbModel = new AppData.DbModel())
            {
                var user = dbModel.Users.Where(x => x.Email == logInModel.Email && x.Password == logInModel.Password).FirstOrDefault();
                if(user == null)
                {
                    ViewBag.Message = "Email or Password is incorect !";
                    return View(logInModel);
                }
                    Session["userId"] = user.Id;
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Authorize", "LogIn");
        }
    }
}