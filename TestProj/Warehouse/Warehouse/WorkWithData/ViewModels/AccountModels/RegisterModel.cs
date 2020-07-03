using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "No email specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password isn`t specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Incorrect password")]
        public string ConfirmPassword { get; set; }

        public string Avatar { get; set; }
    }
}
