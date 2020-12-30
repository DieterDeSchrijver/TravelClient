using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models
{
    public class ObservableWrapper<T> : ObservableObject
    {

        private T _value;

        public T Value
        {
            get { return _value; }
            set { Set("Value", ref _value, value); }
        }
    }
}
