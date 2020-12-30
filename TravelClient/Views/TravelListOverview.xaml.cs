using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelClient.Core.Helpers;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelClient.Views
{
    public sealed partial class TravelListOverview : Page
    {
        public ObservableCollection<TravelList> TravelLists { get; } = new ObservableCollection<TravelList>();

        public TravelListOverview()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {


            base.OnNavigatedTo(e);

            
            var data = await HttpServiceSingleton.GetInstance.GetAsync<List<TravelList>>("TravelList");
            foreach (TravelList list in data)
            {
                TravelLists.Add(list);
            }
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is TravelList item)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(item);
                NavigationService.Navigate<TravelListDetail>(item.Id);
            }
        }

        private void NewListButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate<NewList>();
        }
    }
}
