using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models
{
    public class TravelItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Boolean Completed { get; set; }
        public Category Category { get; set; }
    }
}
