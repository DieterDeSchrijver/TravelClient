using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelClient.Services
{
    public class UserService
    {
        public async void SaveUser(String user)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.TemporaryFolder;
            Windows.Storage.StorageFile currentUser = await storageFolder.GetFileAsync("currentUser");
            await Windows.Storage.FileIO.WriteTextAsync(currentUser, user);
        }
    }
}
