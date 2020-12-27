using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravelClient.Core.Models;

namespace TravelClient.Views
{
    public class TravelListViewModel : INotifyPropertyChanged
    {
        private TravelList _travelList;

        public TravelList TravelList
        {
            get { return _travelList; }
            set
            {
                Set(ref _travelList, value);
            }
        }

        public TravelListViewModel()
        {

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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
