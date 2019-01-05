using GlobalChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlobalChat.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult AddOrEdit()
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddOrEdit(User user)
        {
            var userModel = new AppData.User()
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                DOB  = user.DateOfBirth
            };

            if(string.IsNullOrWhiteSpace(user.Address))
            {
                userModel.Address = "Address not set";
            }
            else
            {
                userModel.Address = user.Address;
            }

            if (string.IsNullOrWhiteSpace(user.Nationality))
            {
                userModel.Nationality = "Nationality not set";
            }
            else
            {
                userModel.Nationality = user.Nationality;
            }

            using (var dbModel = new AppData.DbModel())
            {
                if (dbModel.Users.Any(x => x.Nickname == user.Nickname))
                {
                    ViewBag.DuplicateMessage = "Nickname taken!";
                    return View(user);
                }

                if (dbModel.Users.Any(x => x.Email == user.Email))
                {
                    ViewBag.DuplicateMessage = "Email already exist!";
                    return View(user);
                }

                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";

           return View(new User());
        }
    }
}