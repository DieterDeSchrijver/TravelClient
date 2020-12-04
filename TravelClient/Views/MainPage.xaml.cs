using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelClient.Core.Services;
using TravelClient.Core.Models.requests;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Windows.Security.Credentials;
using TravelClient.Services;

namespace TravelClient.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        HttpDataService http;

        public MainPage()
        {
            InitializeComponent();
            http = new HttpDataService();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            signInErrorMessage.Text = "";
            HttpResponseMessage response = new HttpResponseMessage();
            LoginRequest login = new LoginRequest(emailInput.Text, passwordInput.Text);
            Task task = Task.Run(async () =>
            {
                response = await http.Login<LoginRequest>("https://localhost:5001/api/User/login", login); // sends GET request

            });
            task.Wait(); // Wait
            if (!response.IsSuccessStatusCode)
            {
                signInErrorMessage.Text = response.Content.ReadAsStringAsync().Result;
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

        private void RegisterButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RegisterRequest register = new RegisterRequest(registerEmail.Text, registerPassword.Password, registerName.Text);
            var response = http.PostAsJsonAsync("https://localhost:5001/api/User/Register", register, null);

        }
    }
}
