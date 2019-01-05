using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GlobalChat.Models
{
    public class User
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage ="First Name is required!")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required!")]
        public string LastName { get; set; }

        [DisplayName("Nickname")]
        [Required(ErrorMessage = "Nickname is required!")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required!")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }

        public string Nationality { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}