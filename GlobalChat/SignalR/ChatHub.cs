using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using Newtonsoft.Json;
using GlobalChat.AppData;
using GlobalChat.Models;

namespace GlobalChat.SignalR
{
    public class ChatHub : Hub
    {
        private string ProcessMesssage(string message)
        {
            MessageModel msg = null;

            try
            {
                msg = JsonConvert.DeserializeObject<MessageModel>(message);
            }
            catch (Exception ex)
            {
                return null;
            }

            var dbMessage = new Message()
            {
                Id = Guid.NewGuid(),
                CreatedAtUtc = msg.ToUtcDateTime(),
                CreatedBy = Guid.Parse(msg.UserId),
                Text = msg.Message
            };

            string userName = null;

            using (var dbModel = new AppData.DbModel())
            {
                dbModel.Message.Add(dbMessage);

                try
                {
                    var user = dbModel.User.FirstOrDefault(t => t.Id == dbMessage.CreatedBy);
                    if (user == null)
                    {
                        return null;
                    }

                    userName =user.Nickname;

                    dbModel.SaveChanges();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            var result = new SendMessageModel()
            {
                Id = dbMessage.Id,
                UserId = dbMessage.CreatedBy,
                DateStr = msg.ToUtcDateTime().ToString("o"),
                Name = userName,
                Text = msg.Message
            };

            return JsonConvert.SerializeObject(result);
        }

        public void Send(string message)
        {
            var result = ProcessMesssage(message);

            if (!string.IsNullOrEmpty(result))
            {
                Clients.All.appendMessage(result);
            }
        }
    }
}