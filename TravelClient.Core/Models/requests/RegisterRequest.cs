using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public RegisterRequest(string email, string password, string name)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }
    }
}
