using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models
{
    public class Category: ObservableObject
    {
        private string _name;
        public string Id { get; set; }
        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value);  }
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
