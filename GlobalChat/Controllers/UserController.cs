using GlobalChat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlobalChat.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var userModel = new AppData.User()
            {
                Id = Guid.NewGuid(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Nickname = user.Nickname,
                Email = user.Email,
                Password = user.Password,
                DOB = user.DateOfBirth
            };

            if (string.IsNullOrWhiteSpace(user.Address))
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
                if (dbModel.User.Any(x => x.Nickname == user.Nickname))
                {
                    ViewBag.DuplicateMessage = "Nickname taken!";
                    return View(user);
                }

                if (dbModel.User.Any(x => x.Email == user.Email))
                {
                    ViewBag.DuplicateMessage = "Email already exist!";
                    return View(user);
                }

                dbModel.User.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successfully.";

            return View(new User());
        }

        [HttpGet]
        public ActionResult UserEdit()
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Authorize", "LogIn");
            }

            var currentUserId = (Guid)Session["userId"];

            using (var dbModel = new AppData.DbModel())
            {
                var user = dbModel.User.FirstOrDefault(x => x.Id == currentUserId);
                if(user == null)
                {
                    //TODO
                }

                User userModel = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Nickname = user.Nickname,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                    Nationality = user.Nationality,
                    DateOfBirth = user.DOB.Value
                };

                return View(userModel);
            }
        }

        [HttpPost]
        public ActionResult UserEdit(User user)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Authorize", "LogIn");
            }

            var currentUserId = (Guid)Session["userId"];

            var userModel = new AppData.User()
            {
                Id = currentUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Nickname = user.Nickname,
                Email = user.Email,
                Password = user.Password,
                DOB = user.DateOfBirth
            };

            if (string.IsNullOrWhiteSpace(user.Address))
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
                if (dbModel.User.Any(x => x.Id != currentUserId && x.Nickname == user.Nickname))
                {
                    ViewBag.DuplicateMessage = "Nickname taken!";
                    return View(user);
                }

                if (dbModel.User.Any(x => x.Id != currentUserId && x.Email == user.Email))
                {
                    ViewBag.DuplicateMessage = "Email already exist!";
                    return View(user);
                }

                var dbUser = dbModel.User.FirstOrDefault(x => x.Id == currentUserId);

                dbUser.FirstName = userModel.FirstName;
                dbUser.LastName = userModel.LastName;
                dbUser.Email = userModel.Email;
                dbUser.Password = userModel.Password;
                dbUser.Address = userModel.Address;
                dbUser.Nationality = userModel.Nationality;
                dbUser.DOB = userModel.DOB;

                dbModel.SaveChanges();

                ModelState.Clear();
                ViewBag.SuccessMessage = "Your changes have been saved!";
                return View(user);
            }
        }

        [HttpPost]
        public JsonResult GetUserData([Required]Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return Json(new Response<AppData.User>(null, false, 1));
            }

            using (var dbModel = new AppData.DbModel())
            {
                var user = dbModel.User.FirstOrDefault(t => t.Id == userId);
                if (user == null)
                {
                    return Json(new Response<AppData.User>(null, false, 1));
                }

                user.Message = null;

                return Json(new Response<UserModel>(         
                                new UserModel
                                 {
                                     Id = user.Id,
                                     FullName = $"{user.FirstName} {user.LastName}",
                                     Nickname = user.Nickname,
                                     Email = user.Email,
                                     Address = string.IsNullOrWhiteSpace(user.Address) ? "Not set" : user.Address,
                                     Nationality = string.IsNullOrWhiteSpace(user.Nationality) ? "Not set" : user.Nationality,
                                     DateOfBirth = user.DOB?.ToString("o")
                                 }));
            }
        }
    }
}