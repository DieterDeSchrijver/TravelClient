using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Models;
using TravelClient.Core.Services;
using TravelClient.Services;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace TravelClient.ViewModels
{
    public class TravelListDetailViewModel
    {
        public Geopoint TravelLocation { get; set; }

        HttpDataService http = HttpServiceSingleton.GetInstance;

        public ObservableWrapper<double> Progress { get; set; } = new ObservableWrapper<double>();
        public ObservableCollection<TravelItem> Items { get; set; } = new ObservableCollection<TravelItem>();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        public ObservableWrapper<TravelItem> SelectedItem { get; set; } = new ObservableWrapper<TravelItem>();

        public TravelList TravelList { get; set; }

        public ICommand AddItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        public TravelListDetailViewModel(string id)
        {
            AddItemCommand = new RelayCommand(AddItem);
            DeleteItemCommand = new RelayCommand(DeleteItem);
            Items.CollectionChanged += (sender, e) =>  CalculateProgress(); //TODO trigger ook als item in collectie zelf aanpast
            Task task = Task.Run(async () =>
            {  
                TravelList = await http.GetAsync<TravelList>($"http://localhost:5000/api/TravelList/{id}");
                TravelLocation = new Geopoint(new BasicGeoposition() { Latitude = TravelList.Location.LatCoord, Longitude = TravelList.Location.LngCoord });
                var categories = await http.GetAsync<List<Category>>($"http://localhost:5000/api/User/GetCategories");
                categories.ForEach(x =>
                {
                    Categories.Add(x);
                });

                TravelList.Items.ForEach(item =>
                {
                    Items.Add(item);
                });

            });
            task.Wait();
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PopulateListView()
        {
            Items.OrderBy(i => i.Category.Name).ThenBy(i => i.Completed).ThenBy(i => i.Name).ToList();
            CalculateProgress();
        }

        public void CalculateProgress()
        {
            if (Items.Count(i => i.Completed == false) == 0)
            {
                Progress.Value = 100;
            }
            else
            {
                Progress.Value = (double)Items.Count(i => i.Completed == true) / Items.Count() * 100;
            }
        }


        private async void DeleteItem()
        {
            if (SettingsService.DeleteItemSetting)
            {
                ContentDialog deleteItem = new ContentDialog
                {
                    Title = "Delete item from list",
                    Content = "You can change your preferences in the settings?",
                    CloseButtonText = "Cancel",
                    PrimaryButtonText = "Delete",
                    SecondaryButtonText = "Never show again",
                    DefaultButton = ContentDialogButton.Primary
                };

                ContentDialogResult result = await deleteItem.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    DeleteItemCall();
                }
                if (result == ContentDialogResult.Secondary)
                {
                    SettingsService.ChangeDeleteItemSetting(false);
                    DeleteItemCall();
                }
                if (result == ContentDialogResult.None)
                {

                }

            }
            if (!SettingsService.DeleteItemSetting)
            {
                DeleteItemCall();
            }
        }


        private void DeleteItemCall()
        {
            Items.Remove(SelectedItem.Value);
            //TODO HTTP
            PopulateListView();
        }

        /*  private void checkbox_toggle(object sender, RoutedEventArgs e)
          {
              PopulateListView();
          }
        */
        private void AddItem()
        {
            //TODO HTTP
            var newItemName = new TextBox();
            if (String.IsNullOrEmpty(newItemName.Text) /*|| cmbox.SelectedItem == null*/)
            {
             //   addItemErrorMessage.Text = "please fill in all fields.";
            }
            else
            {

            }
        }

    }
}

