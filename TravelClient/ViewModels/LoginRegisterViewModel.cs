using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Models.requests;
using TravelClient.Core.Services;
using TravelClient.Services;
using TravelClient.Views;

namespace TravelClient.ViewModels
{

    public class LoginRegisterViewModel
    {
        public HttpDataService http { get; set; } = new HttpDataService();
        public LoginRequestModel LoginModel { get; set; }
        public RegisterRequestModel RegisterModel { get; set; }

        #region Commands
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        public LoginRegisterViewModel()
        {
            this.LoginModel = new LoginRequestModel("", "");
            this.RegisterModel = new RegisterRequestModel("", "", "");
            LoginCommand = new RelayCommand(Login, true);
        }

        private void Register()
        {
            var response = http.PostAsJsonAsync("https://localhost:5001/api/User/Register", RegisterModel, null);
        }

        private void Login() {
//             signInErrorMessage.Text = "";
             HttpResponseMessage response = new HttpResponseMessage();
             Task task = Task.Run(async () =>
             {
                  response = await http.Login<LoginRequestModel>("https://localhost:5001/api/User/login", LoginModel); // sends GET request

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
                     Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                     Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
                     await Windows.Storage.FileIO.WriteTextAsync(currentUser, response.Content.ReadAsStringAsync().Result);
                 });
                 t.Wait();
                 ShellPage.Current.ShowPanel();
                 NavigationService.Frame.Navigate(typeof(TravelListOverview));
             }
        }
    }
}
