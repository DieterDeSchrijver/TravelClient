using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Helpers;
using TravelClient.Core.Models;
using TravelClient.Core.Services;
using TravelClient.Services;

namespace TravelClient.ViewModels
{
    public class CategoriesViewModel
    {
        public ObservableString NewCategoryName { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public CategoriesViewModel()
        {
            NewCategoryName = new ObservableString();
            Categories =  new ObservableCollection<Category>();
            AddCategoryCommand = new RelayCommand(AddCategory, true);
            FetchCategories();

        }

        public async void FetchCategories()
        {
            var x = await HttpServiceSingleton.GetInstance.GetAsync<List<Category>>($"User/GetCategories");
            Categories.Clear();
            x.ForEach(a =>
            {
                Categories.Add(a);
            });

        }

        private async void AddCategory()
        {
            string response = await HttpServiceSingleton.GetInstance.PostAsJsonAsync("User/AddCategory", new Category(NewCategoryName.Value));
            FetchCategories();
            NewCategoryName.Value = "";
        }
    }
}
