using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Carpool_App.Classes
{
    internal class GtfsAPI
    {
        private readonly HttpClient client;

        public GtfsAPI()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", "fede8163155c4d4e9d0f89dc8d94d0e7");
        }

        public async Task<string> GetRealTimeUpdates()
        {

            HttpResponseMessage response = await client.GetAsync("https://api.nationaltransport.ie/gtfsr/v2/TripUpdates?format=json");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return data;
            }
            else
            {
                // Handle error
                return $"Error: {response.StatusCode}";
            }
        }
    }
}
