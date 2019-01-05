using System;

namespace GlobalChat.Models
{
    public class SendMessageModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string DateStr { get; set; }
    }
}