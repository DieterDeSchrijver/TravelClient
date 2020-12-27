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
        public ObservableString NewCategoryName { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ObservableCollection<Category> Categories { get; set; } 
        HttpDataService http = new HttpDataService();
        public string s = String.Empty;
        public CategoriesViewModel()
        {
            NewCategoryName = new ObservableString();
            Categories =  new ObservableCollection<Category>();
            AddCategoryCommand = new RelayCommand(AddCategory, true);
            Task task = Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
                s = await Windows.Storage.FileIO.ReadTextAsync(currentUser);
            });
            task.Wait(); // Wait
            FetchCategories();

        }

        public async void FetchCategories()
        {
            var x = await http.GetAsync<List<Category>>($"http://localhost:5000/api/User/GetCategories", s);
            Categories.Clear();
            x.ForEach(a =>
            {
                Categories.Add(a);
            });

        }

        private async void AddCategory()
        {
            string response = await http.PostAsJsonAsync("http://localhost:5000/api/User/AddCategory", new Category(NewCategoryName.Value), s);
            FetchCategories();
            NewCategoryName.Value = "";
        }
    }
}
