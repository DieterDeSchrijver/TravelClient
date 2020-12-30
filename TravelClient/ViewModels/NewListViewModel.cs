using GalaSoft.MvvmLight.Command;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Helpers;
using TravelClient.Core.Models;
using TravelClient.Core.Models.requests;
using TravelClient.Core.Services;
using TravelClient.Services;
using TravelClient.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelClient.ViewModels
{
    public class NewListViewModel
    {
        private string _autoSuggestionBoxText;
        public ObservableCollection<String> Suggestions { get; set; } = new ObservableCollection<string>();
        public NewListRequest NewListRequest { get; set; } = new NewListRequest();
        public ObservableWrapper<BitmapImage> Image { get; set; } = new ObservableWrapper<BitmapImage>();

        List<LocObj> locs = new List<LocObj>();

        HttpDataService http = Singleton<HttpDataService>.Instance;

        public string AutoSuggestionBoxText
        {
            get { return _autoSuggestionBoxText; }
            set
            {
                if (this._autoSuggestionBoxText != value)
                {
                    this._autoSuggestionBoxText = value;
                    this.LocationAutoSuggestTextChanged();
                }
            }
        }

        public ObservableWrapper<string> LocationSuggestBox { get; set; } = new ObservableWrapper<string>();

        public ICommand AddList;
        public ICommand Query;

        public NewListViewModel()
        {
            AddList = new RelayCommand(CreateList);
            Query = new DelegateCommand<AutoSuggestBoxQuerySubmittedEventArgs>(LocationAutoSuggest_QuerySubmitted);
        }
        public void LocationAutoSuggestTextChanged()
        {
                if (this._autoSuggestionBoxText.Length > 1)
                {
                    this.GetSuggestionsAsync(this._autoSuggestionBoxText);
                }
                else
                {
                Suggestions.Clear();
                Suggestions.Add("No suggestions...");
                }
           
        }

        private void GetSuggestionsAsync(string text)
        {
            Task t = Task.Run(async () =>
            {
                locs = await http.GetLocationAsync($"https://api.mapbox.com/geocoding/v5/mapbox.places/{text}.json?access_token=pk.eyJ1IjoiZGlldGVyZHMiLCJhIjoiY2tpMGRlMTVmMDBvMjMwa2JveWYwY3k3eSJ9.I9Y9bm0oMnQJyshyZTKMdQ&autocomplete=true&limit=5");
            });
            t.Wait();
            Suggestions.Clear();
            foreach (var item in locs)
            {
                Suggestions.Add(item.LocationNameLong);
            }

        }

        public async void LocationAutoSuggest_QuerySubmitted(AutoSuggestBoxQuerySubmittedEventArgs arg)
        {
            if (arg.ChosenSuggestion != null && arg.ChosenSuggestion.ToString() != "No suggestions...")
            {
                LocationSuggestBox.Value = arg.ChosenSuggestion.ToString();
                LocObj location = locs.Find(l => l.LocationNameLong == arg.ChosenSuggestion.ToString());
                var chosenLocImage = await http.GetImageAsync($"https://api.unsplash.com/search/photos?query={location.LocationNameShort}&client_id=TiZc8-TmzwTTAO3vLdLw4PNX6nzwtFnF7UKotCLsumU&per_page=1");
                Image.Value = new BitmapImage(new Uri(chosenLocImage)); 
            }
            else
            {
                //LocationSuggestBox.Value = sender.Text;
            }
        }

        private async void CreateList()
        {
            string response = await http.PostAsJsonAsync("TravelList/CreateTravelList", AddList, null);
            NavigationService.Navigate<TravelListDetail>(response);
        }
    }
}
