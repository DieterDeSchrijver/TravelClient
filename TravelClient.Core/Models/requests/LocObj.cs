using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models.requests
{
    public class LocObj
    {
        public string LocationNameLong { get; set; }
        public string LocationNameShort { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }

        public LocObj(string locationNameLong, string locationNameShort, float lon, float lat)
        {
            this.LocationNameLong = locationNameLong;
            this.LocationNameShort = locationNameShort;
            this.Lon = lon;
            this.Lat = lat;
        }

        public LocObj()
        {
        }
    }
}
