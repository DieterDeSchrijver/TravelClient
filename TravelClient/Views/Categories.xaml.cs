using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelClient.Core.Models;
using TravelClient.Core.Services;
using TravelClient.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace TravelClient.Views
{

    public sealed partial class Categories : Page
    {
        public CategoriesViewModel ViewModel { get; set; } = new CategoriesViewModel();

        public Categories()
        {
            this.InitializeComponent();
        }
    }
    }
