using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class User : Entity
    {
        public User() : base(Utils.GenerateID()) { }

        public User(string email, string password, string avatar = null) : this()
        {
            Email = email;
            Password = password;
            Role = "user";
            Avatar = avatar;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Avatar { get; set; }
    }
}
