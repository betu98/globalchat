using GlobalChat.AppData;
using System;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using GlobalChat.Models;

namespace GlobalChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var dbModel = new AppData.DbModel())
            {
                try
                {
                    var messages = JsonConvert.SerializeObject(dbModel.Message.Include($"{nameof(Message.User)}").OrderBy(t => t.CreatedAtUtc).ToArray().Select(t =>
                         new SendMessageModel()
                         {
                             Id = t.Id,
                             DateStr = t.CreatedAtUtc.ToString("o"),
                             Name = t.User.Nickname,
                             Text = t.Text,
                             UserId = t.CreatedBy
                         }
                    ).ToArray());

                    ViewBag.Messages = messages;
                }
                catch (Exception)
                {

                }
            }

            return View();
        }
    }
}