using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelClient.Core.Helpers;
using TravelClient.Core.Services;

namespace TravelClient.Services
{
    public class HttpServiceSingleton
    {
        public static HttpDataService GetInstance { get; set; } = Singleton<HttpDataService>.Instance;
        UserService userService = Singleton<UserService>.Instance;
    }
}
