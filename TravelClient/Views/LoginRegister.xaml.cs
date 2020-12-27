using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;
using TravelClient.ViewModels;

namespace TravelClient.Views
{
    public sealed partial class LoginRegister : Page
    {

        public LoginRegisterViewModel ViewModel { get; set; } = new LoginRegisterViewModel();

        public LoginRegister()
        {
            InitializeComponent();
        }
    }
}
