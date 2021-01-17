using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Models;
using TravelClient.Services;
using TravelClient.Views;

namespace TravelClient.ViewModels
{
    public class TravelListOverviewViewModel
    {
        public ObservableCollection<TravelList> TravelLists { get; } = new ObservableCollection<TravelList>();

        public ICommand MakeNewListCommand { get; set; }
        public ICommand NavigateToItemDetails { get; set; }
        public TravelListOverviewViewModel()
        {
            InitializeData();
            MakeNewListCommand = new RelayCommand(NavigateToNewListView);
        }

        private void NavigateToNewListView()
        {
            NavigationService.Navigate<NewList>();
        }

        private async Task InitializeData()
        {
            var data = await HttpServiceSingleton.GetInstance.GetAsync<List<TravelList>>("TravelList");
            foreach (TravelList list in data)
            {
                TravelLists.Add(list);
            }
        }
    }
}
