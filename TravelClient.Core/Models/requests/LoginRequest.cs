using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginRequest(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
