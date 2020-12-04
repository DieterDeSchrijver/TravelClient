using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class NewListRequest
    {
        public string Listname { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public float LngCoord { get; set; }
        public float LatCoord { get; set; }
        public string Image { get; set; }

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
