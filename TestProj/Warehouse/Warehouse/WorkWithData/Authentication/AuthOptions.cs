using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class AuthOptions
    {
        public const string ISSUER = "Warehouse";
        public const string AUDIENCE = "http://localhost:5000/";
        const string KEY = "Simple_111_Key_!_!";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
