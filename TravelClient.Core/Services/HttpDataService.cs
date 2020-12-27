using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using TravelClient.Core.Models.requests;

namespace TravelClient.Core.Services
{

    // This class provides a wrapper around common functionality for HTTP actions.
    // Learn more at https://docs.microsoft.com/windows/uwp/networking/httpclient
    public class HttpDataService
    {
        private readonly Dictionary<string, object> responseCache;
        private HttpClient client;
        private HttpClient baselessClient;
        

        public HttpDataService()
        {
            client = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            });

            
           client.BaseAddress = new Uri("http://localhost:5000/api/");
            

            responseCache = new Dictionary<string, object>();

            baselessClient = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string uri, string accessToken = null)
        {
            if (!String.IsNullOrEmpty(accessToken))
            {
                AddAuthorizationHeader(accessToken);
            }
                T result = default;
                var json = await client.GetStringAsync(uri);
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
                return result;
        }

        public async Task<List<LocObj>> GetLocationAsync(string uri)
        {
            var json = await baselessClient.GetStringAsync(uri);

            dynamic model = await Task.Run(() => JsonConvert.DeserializeObject(json));

            List<LocObj> locations = new List<LocObj>();

            foreach (dynamic location in model["features"])
            {
                string x = location["place_name"];
                string y = location["text"];
                float lon = location["center"][0];
                float lat = location["center"][1];
                LocObj ob = new LocObj(x, y, lon, lat);
                locations.Add(ob);
            }
            return locations;
        }

        public async Task<string> GetImageAsync(string v)
        {
            var json = await baselessClient.GetStringAsync(v);

            dynamic model = await Task.Run(() => JsonConvert.DeserializeObject(json));

            return model["results"][0]["urls"]["small"];
        }

        public async Task<HttpResponseMessage> Login<T>(string uri, T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var response = await client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
            return response;

        }

        public async Task<bool> PostAsync<T>(string uri, T item)
        {
            if (item == null)
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PostAsync(uri, byteContent);

            return response.IsSuccessStatusCode;
        }

        

        public async Task<string> PostAsJsonAsync<T>(string uri, T item, string jwt = null)
        {

            if (jwt != null)
            {
                AddAuthorizationHeader(jwt);
            }
            

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync(uri, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.Content.ReadAsStringAsync().Result;
        }

        public async Task<bool> PutAsync<T>(string uri, T item)
        {
            if (item == null)
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(uri, byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutAsJsonAsync<T>(string uri, T item)
        {
            if (item == null)
            {
                return false;
            }

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PutAsync(uri, new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string uri)
        {
            var response = await client.DeleteAsync(uri);

            return response.IsSuccessStatusCode;
        }

        // Add this to all public methods
        private void AddAuthorizationHeader(string token)
        {
            token = token.Replace("\"", "");
            if (string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = null;
                return;
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
