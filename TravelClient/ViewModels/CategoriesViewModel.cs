using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Category NewCategory;
        public ICommand AddCategoryCommand { get; set; }
        public List<Category> Categories { get; set; } 
        HttpDataService http = new HttpDataService();
        public string s = String.Empty;
        public CategoriesViewModel()
        {
            Categories =  new List<Category>();
            NewCategory = new Category("");
            AddCategoryCommand = new RelayCommand(AddCategory, true);
            Task task = Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
                s = await Windows.Storage.FileIO.ReadTextAsync(currentUser);
            });
            //task.Wait(); // Wait
            FetchCategories();
        }

        public async void FetchCategories()
        {
            Categories.Add(NewCategory);

        }

        private async void AddCategory()
        {
            string response = await http.PostAsJsonAsync("http://localhost:5000/api/User/AddCategory", NewCategory, s);
            FetchCategories();
            NewCategory = new Category("");
        }
    }
}
