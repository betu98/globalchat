using System;

namespace GlobalChat.Models
{
    public class MessageModel
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public string LocalTime { get; set; }

        public DateTime ToUtcDateTime()
        {
            DateTime lTime;

            bool result = DateTime.TryParse(LocalTime, out lTime);

            return result ? lTime : DateTime.MinValue;
        }
    }
}