using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TravelClient.Services
{
    public static class SettingsService
    {
        public static bool DeleteItemSetting { get; set; } = true;
        private static bool _isLoggedIn = false;
        public static bool IsLoggedIn { get; set; } = false;


        public static void InitializeAsync()
        {
            DeleteItemSetting = LoadFromSettingsAsync();
        }

        private static bool LoadFromSettingsAsync()
        {
            bool x = DeleteItemSetting;
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            string localValue = localSettings.Values["deleteItemSetting"] as string;
            if (localValue == null)
            {
                return x;
            }
            return bool.Parse(localValue);
        }

        internal static void ChangeDeleteItemSetting(bool v)
        {
            DeleteItemSetting = v;
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["deleteItemSetting"] = v.ToString();
        }
    }
}
