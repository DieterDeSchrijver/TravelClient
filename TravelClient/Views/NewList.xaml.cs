using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelClient.Core.Models.requests;
using TravelClient.Core.Services;
using TravelClient.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelClient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewList : Page, INotifyPropertyChanged
    {
        List<LocObj> locs = new List<LocObj>();
        HttpDataService http = new HttpDataService();
        LocObj chosenLoc;
        string chosenLocImage;
        string s;

        public event PropertyChangedEventHandler PropertyChanged;

        public NewList()
        {
            this.InitializeComponent();

            Task task = Task.Run(async () =>
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
                Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
                s = await Windows.Storage.FileIO.ReadTextAsync(currentUser);
            });
            task.Wait(); // Wait
        }

        public void LocationAutoSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (sender.Text.Length > 1)
                {
                    sender.ItemsSource = this.GetSuggestionsAsync(sender.Text);
                }
                else
                {
                    sender.ItemsSource = new string[] { "No suggestions..." };
                }
            }
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

        public async void LocationAutoSuggest_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs arg)
        {
            if (arg.ChosenSuggestion != null && arg.ChosenSuggestion.ToString() != "No suggestions...")
            {
                LocationSuggestBox.Text = arg.ChosenSuggestion.ToString();
                chosenLoc = locs.Find(l => l.LocationNameLong == arg.ChosenSuggestion.ToString());
                chosenLocImage = await http.GetImageAsync($"https://api.unsplash.com/search/photos?query={chosenLoc.LocationNameShort}&client_id=TiZc8-TmzwTTAO3vLdLw4PNX6nzwtFnF7UKotCLsumU&per_page=1");
                image.Source = new BitmapImage(new Uri(chosenLocImage)); ;
            }
            else
            {
                LocationSuggestBox.Text = sender.Text;
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            NewListRequest request = new NewListRequest(listNameInput.Text, startDate.Date.Value.DateTime, endDate.Date.Value.DateTime, descriptionInput.Text, chosenLoc.LocationNameLong,chosenLoc.Lon, chosenLoc.Lat, chosenLocImage);
            string response = await http.PostAsJsonAsync("https://localhost:5001/api/TravelList/CreateTravelList", request, s);
            NavigationService.Navigate<TravelListDetail>(response);
        }
    }
}
