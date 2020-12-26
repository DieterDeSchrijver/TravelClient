using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelClient.Core.Models;
using TravelClient.Core.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Categories : Page, INotifyPropertyChanged
    {
        HttpDataService http = new HttpDataService();
        private List<Category> _categories;
        public string s;
        public List<Category> Categoriess
        {
            get { return _categories; }
            set { Set(ref _categories, value); }
        }

        public Categories()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            s = "";
            var id = e.Parameter;

            Task task = Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
                s = await Windows.Storage.FileIO.ReadTextAsync(currentUser);
            });
            task.Wait(); // Wait     
            Categoriess = await http.GetAsync<List<Category>>($"http://localhost:5000/api/User/GetCategories", s);
        }

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Category c = new Category(newCategory.Text);
            string response = await http.PostAsJsonAsync("http://localhost:5000/api/User/AddCategory", c, s);
            Categoriess = await http.GetAsync<List<Category>>($"http://localhost:5000/api/User/GetCategories", s);
        }
    }
    }
