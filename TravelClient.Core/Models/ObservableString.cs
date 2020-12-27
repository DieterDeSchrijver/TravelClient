using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models
{
    public class ObservableString : ObservableObject
    {

        private string _value;

        public string Value
        {
            get { return _value; }
            set { Set("Value", ref _value, value); }
        }
    }
}
