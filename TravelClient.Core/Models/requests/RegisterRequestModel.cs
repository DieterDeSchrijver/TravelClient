using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public RegisterRequestModel(string email, string password, string name)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }
    }
}
