using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelClient.Core.Models;
using TravelClient.Core.Models.requests;
using TravelClient.Core.Services;
using TravelClient.Services;
using TravelClient.Views;

namespace TravelClient.ViewModels
{
    public class NewListViewModel
    {
        private string _autoSuggestionBoxText;
        public String[] Suggestions { get; set; } = new string[0];
        public NewListRequest NewListRequest { get; set; } = new NewListRequest();

        List<LocObj> locs = new List<LocObj>();
        HttpDataService http = new HttpDataService();
        LocObj chosenLoc;
        string chosenLocImage;
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

        public ObservableString LocationSuggestBox { get; set; } = new ObservableString();

        public ICommand AddList;

        public NewListViewModel()
        {
            AddList = new RelayCommand(CreateList, true);
        }
        public void LocationAutoSuggestTextChanged()
        {
                if (this._autoSuggestionBoxText.Length > 1)
                {
                    Suggestions = this.GetSuggestionsAsync(this._autoSuggestionBoxText);
                }
                else
                {
                    Suggestions = new string[] { "No suggestions..." };
                }
           
        }

        private string[] GetSuggestionsAsync(string text)
        {
            Task t = Task.Run(async () =>
            {
                locs = await http.GetLocationAsync($"https://api.mapbox.com/geocoding/v5/mapbox.places/{text}.json?access_token=pk.eyJ1IjoiZGlldGVyZHMiLCJhIjoiY2tpMGRlMTVmMDBvMjMwa2JveWYwY3k3eSJ9.I9Y9bm0oMnQJyshyZTKMdQ&autocomplete=true&limit=5");
            });
            t.Wait();

            List<string> s = new List<string>();
            foreach (var item in locs)
            {
                s.Add(item.LocationNameLong);
            }

            return s.ToArray();
        }

   /*     public async void LocationAutoSuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs arg)
        {
            if (arg.ChosenSuggestion != null && arg.ChosenSuggestion.ToString() != "No suggestions...")
            {
                LocationSuggestBox.Value = arg.ChosenSuggestion.ToString();
                chosenLoc = locs.Find(l => l.LocationNameLong == arg.ChosenSuggestion.ToString());
                chosenLocImage = await http.GetImageAsync($"https://api.unsplash.com/search/photos?query={chosenLoc.LocationNameShort}&client_id=TiZc8-TmzwTTAO3vLdLw4PNX6nzwtFnF7UKotCLsumU&per_page=1");
                image.Source = new BitmapImage(new Uri(chosenLocImage)); ;
            }
            else
            {
                LocationSuggestBox.Value = sender.Text;
            }
        }*/

        private async void CreateList()
        {
            string response = await http.PostAsJsonAsync("http://localhost:5000/api/TravelList/CreateTravelList", AddList, null);
            NavigationService.Navigate<TravelListDetail>(response);
        }
    }
}
