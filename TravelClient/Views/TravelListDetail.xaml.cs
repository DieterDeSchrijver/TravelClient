using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelClient.Core.Models;
using TravelClient.Core.Services;
using TravelClient.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https ://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListDetail : Page, INotifyPropertyChanged
    {
        private TravelList _travelList;
        private List<TravelItem> _items;
        private List<Category> _categories;

        public List<TravelItem> Items
        {
            get { return _items; }
            set { Set(ref _items, value); }
        }


        public TravelList TravelList
        {
            get { return _travelList; }
            set { Set(ref _travelList, value); }
        }

        public List<Category> Categories
        {
            get { return _categories; }
            set { Set(ref _categories, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TravelListDetail()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id = e.Parameter;    

            TravelList = await HttpServiceSingleton.GetInstance.GetAsync<TravelList>($"TravelList/{id}");
            Categories = await HttpServiceSingleton.GetInstance.GetAsync<List<Category>>($"User/GetCategories");
            Items = TravelList.Items;

            PopulateListView();
        }

        private void PopulateListView()
        {
            errorMessage.Text = "";
            Items = Items.OrderBy(i => i.Completed).ThenBy(i => i.Name).ToList(); 
            
            CalculateProgress();
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

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(TravelList);
            }
        }

        public void CalculateProgress()
        {
            if (Items.Count(i => i.Completed == false) == 0)
            {
                bar.Value = 100;
            }
            else
            {
                bar.Value = (double)Items.Count(i => i.Completed == true) / Items.Count() * 100;
            }
        }



        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            addPane.IsPaneOpen = !addPane.IsPaneOpen;
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemListView.SelectedItem == null)
            {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            }
            else
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
                        DeleteItem();
                    }
                    if (result == ContentDialogResult.Secondary)
                    {
                        SettingsService.ChangeDeleteItemSetting(false);
                        DeleteItem();
                    }
                    if (result == ContentDialogResult.None)
                    {
                        
                    }

                }
                if (!SettingsService.DeleteItemSetting)
                {
                    DeleteItem();
                }
            }            
        }

        private void DeleteItem()
        {
                TravelItem ti = (TravelItem)itemListView.SelectedItem;
                Items.Remove(ti);
                //TODO HTTP
                PopulateListView();
        }

        private void checkbox_toggle(object sender, RoutedEventArgs e)
        {
            PopulateListView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(newItemName.Text) || cmbox.SelectedItem == null)
            {
                addItemErrorMessage.Text = "please fill in all fields.";
            }
            else
            {
                
            }
        }
    }
}
