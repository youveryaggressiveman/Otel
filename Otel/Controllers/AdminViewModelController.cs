using Otel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class AdminViewModelController
    {
        public async Task<Hotel> CreateHotel(Hotel newHotel)
        {
            var JsonObject = JsonSerializer.Serialize<Hotel>(newHotel);
            var url = "http://localhost:63262/api/Otels";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<Hotel>(await result.Content.ReadAsStringAsync());

            client.Dispose();

            return parsedResult;
        }

        public async Task<List<TypeRoom>> GetAllTypeRoomList()
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/TypeRooms");

            var result = JsonSerializer.Deserialize<List<TypeRoom>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Currency>> GetAllCurrencyList()
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Currencies");

            var result = JsonSerializer.Deserialize<List<Currency>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Country>> GetAllCountryList()
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Countries");

            var result = JsonSerializer.Deserialize<List<Country>>(stringTask);

            client.Dispose();

            return result;
        }
    }
}

