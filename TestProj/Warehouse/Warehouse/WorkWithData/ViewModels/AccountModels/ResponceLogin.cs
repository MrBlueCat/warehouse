using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Warehouse
{
    public class ResponceLogin
    {
        public ResponceLogin(string token, string email, string role, string avatar = null)
        {
            Token = token;
            Email = email;
            Role = role;
            Avatar = avatar;
        }

        public string Token { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
        public string Avatar { get; set; }
    }
}
