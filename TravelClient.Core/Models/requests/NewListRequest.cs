using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class NewListRequest : ObservableObject
    {
        private string _listName;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _description;
        private string _name;
        private float _lngCoord;
        private float _latCoord;
        private string _image;
        public string Listname 
        {
            get { return _listName; }
            set { Set("ListName", ref _listName, value); }
           }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { Set("StartDate", ref _startDate, value); }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set { Set("EndDate", ref _endDate, value); }
        }
        public string Description
        {
            get { return _description; }
            set { Set("Description", ref _description, value); }
        }
        public string Name
        {
            get { return _name; }
            set { Set("Name", ref _name, value); }
        }
        public float LngCoord
        {
            get { return _lngCoord; }
            set { Set("LngCoord", ref _lngCoord, value); }
        }
        public float LatCoord
        {
            get { return _latCoord; }
            set { Set("LatCoord", ref _latCoord, value); }
        }
        public string Image
        {
            get { return _image; }
            set { Set("Image", ref _image, value); }
        }

        public NewListRequest()
        {
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
        }

        public NewListRequest(string listname, DateTime startDate, DateTime endDate, string description, string name, float lngCoord, float latCoord, string image)
        {
            Listname = listname;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            Name = name;
            LngCoord = lngCoord;
            LatCoord = latCoord;
            Image = image;
        }
    }
}
