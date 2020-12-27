using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Models;
using TravelClient.Core.Services;

namespace TravelClient.ViewModels
{
    public class CategoriesViewModel
    {
        /*  
          private List<Category> _categories = new List<Category>();
          private string s { get; set; } = String.Empty;
          public List<Category> Categories
          {
              get { return _categories; }
              set { _categories = value; }
          }*/
        public ICommand AddCategoryCommand { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        HttpDataService http = new HttpDataService();
        public string s = "";
        public CategoriesViewModel()
        {

            AddCategoryCommand = new RelayCommand(FetchCategories, true);
            /*Task task = Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
                s = await Windows.Storage.FileIO.ReadTextAsync(currentUser);
            });
            task.Wait(); // Wait     */

        }

        public void FetchCategories()
        {
            Categories = http.GetAsync<List<Category>>($"https://localhost:5000/api/User/GetCategories", s).Result;
        }

/*          private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Category c = new Category(newCategory.Text);
            string response = await http.PostAsJsonAsync("https://localhost:5000/api/User/AddCategory", c, s);
            Categories = await http.GetAsync<List<Category>>($"https://localhost:5000/api/User/GetCategories", s);

        }*/
    }
}
