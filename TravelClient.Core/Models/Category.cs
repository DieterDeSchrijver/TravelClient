using System;
using System.Collections.Generic;
using System.Text;

namespace TravelClient.Core.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Category(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
