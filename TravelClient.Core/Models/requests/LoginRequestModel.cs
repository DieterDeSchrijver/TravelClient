using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class LoginRequestModel : ObservableObject
    {
        private string _email = "dieter@gmail.com";
        private string _password = "test";
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Set("Email", ref _email, value);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                Set("Password", ref _password, value);
            }
        }
        public LoginRequestModel(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
