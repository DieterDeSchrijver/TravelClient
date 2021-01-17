using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelClient.Views.Dialogs
{
    public sealed partial class MapDialog : ContentDialog
    {
        public Geopoint UserLocation { get; set; }
        public Geopoint Location { get; set; }
        public MapRouteView RouteView { get; set; }
        public MapDialog()
        {
            this.InitializeComponent();
        }

        public MapDialog(Geopoint location, Geopoint userlocation)
        {
            this.InitializeComponent();
            Map = FindName("Map") as MapControl;
            Height = 600;
            Width = 4000;
            Location = location;
            UserLocation = userlocation;
            CalculateRouteAsync();
        }

        public async Task CalculateRouteAsync()
        {
          MapRouteFinderResult routeResult =
            await MapRouteFinder.GetDrivingRouteAsync(
            Location,
            UserLocation,
            MapRouteOptimization.Time,
            MapRouteRestrictions.None);
            RouteView = new MapRouteView(routeResult.Route);
            Map.Routes.Add(RouteView);

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
