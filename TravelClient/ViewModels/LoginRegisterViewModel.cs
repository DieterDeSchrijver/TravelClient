using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Helpers;
using TravelClient.Core.Models.requests;
using TravelClient.Core.Services;
using TravelClient.Services;
using TravelClient.Views;

namespace TravelClient.ViewModels
{

    public class LoginRegisterViewModel
    {
        public LoginRequestModel LoginModel { get; set; }
        public RegisterRequestModel RegisterModel { get; set; }
        public UserService userService = Singleton<UserService>.Instance;

        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        public LoginRegisterViewModel()
        {
            this.LoginModel = new LoginRequestModel("", "");
            this.RegisterModel = new RegisterRequestModel("", "", "");
            LoginCommand = new RelayCommand(Login, true);
            RegisterCommand = new RelayCommand(Register, true);
        }

        private void Register()
        {
            var response = HttpServiceSingleton.GetInstance.PostAsJsonAsync("User/Register", RegisterModel);
        }

        private void Login() {
//             signInErrorMessage.Text = "";
             HttpResponseMessage response = new HttpResponseMessage();
             Task task = Task.Run(async () =>
             {
                  response = await HttpServiceSingleton.GetInstance.Login<LoginRequestModel>("User/login", LoginModel); // sends GET request

             });
             task.Wait(); // Wait
             if (!response.IsSuccessStatusCode)
             {
                // signInErrorMessage.Text = response.Content.ReadAsStringAsync().Result;
             }
             else
             {
                 Task t = Task.Run(async () =>   
                 {
                     userService.SaveUser(response.Content.ReadAsStringAsync().Result);
                     HttpServiceSingleton.GetInstance.AddAuthorizationHeader(response.Content.ReadAsStringAsync().Result);
                 });
                 t.Wait();
                 ShellPage.Current.ShowPanel();
                 NavigationService.Frame.Navigate(typeof(TravelListOverview));
             }
        }
    }
}
