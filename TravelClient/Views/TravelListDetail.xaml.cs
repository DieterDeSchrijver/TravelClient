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
using TravelClient.ViewModels;
using Windows.Devices.Geolocation;
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
    public sealed partial class TravelListDetail : Page
    {
        public TravelListDetailViewModel ViewModel { get; set; }
        public TravelListDetail()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id =(string) e.Parameter;
            var accessStatusTask = Geolocator.RequestAccessAsync().AsTask();
            accessStatusTask.Wait();
            Geoposition pos = null;
            if(accessStatusTask.Result == GeolocationAccessStatus.Allowed)
            {
                Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 1 };
                var task = geolocator.GetGeopositionAsync().AsTask();
                task.Wait();
                pos = task.Result;
            }

            ViewModel = new TravelListDetailViewModel(id, pos);
        }
    }
}
