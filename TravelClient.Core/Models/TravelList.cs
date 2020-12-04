using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TravelClient.Core.Models
{
    public class TravelList
    {
        public string Id { get; set; }
        public string Listname;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public List<TravelItem> Items { get; set; }
        public Location Location { get; set; }


        public TravelList() { }

    }
}
