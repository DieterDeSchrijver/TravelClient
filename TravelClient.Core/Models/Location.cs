using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelClient.Core.Models
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public float LngCoord { get; set; }
        public float LatCoord { get; set; }
        public DateTime LocalTime { get; set; }
        public string ImageURL { get; set; }
        public BitmapImage ImageSource {
             get {
                var x = ImageURL ?? "https://unsplash.com/photos/okz_PbR7yl4w";
                return new BitmapImage(new Uri(x));
            }
        }
            

        public Location(string id, string name, float lngCoord, float latCoord, string image)
        {
            Id = id;
            Name = name;
            LngCoord = lngCoord;
            LatCoord = latCoord;
            ImageURL = image;
        }
    }
}
