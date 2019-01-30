using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalChat.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Nickname { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Nationality { get; set; }

        public string DateOfBirth { get; set; }
    }
}