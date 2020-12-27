using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelClient.Core.Models
{
    public class ObservableBitmapImage : ObservableObject
    {
        private BitmapImage _value;

        public BitmapImage Value
        {
            get { return _value; }
            set { Set("Value", ref _value, value); }
        }
    }
}
