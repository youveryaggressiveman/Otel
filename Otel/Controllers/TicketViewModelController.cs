using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class TicketViewModelController
    {
        public async Task<List<Room>> GetNumerByTypeRoomId(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Rooms/typeRoomsId?id=" + id);

            var result = JsonSerializer.Deserialize<List<Room>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<AddressOfOtel> GetAddressOfOtelBySelectedOtel(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/addressOfOtel?otelID=" + id);

            var result = JsonSerializer.Deserialize<List<AddressOfOtel>>(stringTask);

            client.Dispose();

            return result[0];
        }

        public async Task<Hotel> GetHotelByOtelName(string name)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/name?otelName=" + name);

            var result = JsonSerializer.Deserialize<Hotel>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<ImageOfOtel> GetIamgeBySelectedOtel(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/image?otelID=" + id);

            var result = JsonSerializer.Deserialize<List<ImageOfOtel>>(stringTask);

            client.Dispose();

            return result[0];
        }

        public async Task<Description> GetDescriptionBySelectedOtel(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/Description?otelID=" + id);

            var result = JsonSerializer.Deserialize<List<Description>>(stringTask);

            client.Dispose();

            return result[0];
        }

        public async Task<Date> CreateDate(Date newDate)
        {
            var JsonObject = JsonSerializer.Serialize<Date>(newDate);
            var url = "http://localhost:63262/api/Date";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<Date>(await result.Content.ReadAsStringAsync());

            return parsedResult;
        }

        public async Task<List<NameOtel>> GetNameOtlelByOtelList(List<Hotel> hotels)
        {
            HttpClient client = new HttpClient();
            List<NameOtel> nameOtels = new List<NameOtel>();

            foreach (var item in hotels)
            {
                var stringTask = await client.GetStringAsync("http://localhost:63262/api/NameOtels?id=" + item.NameOtelID);

                var result = JsonSerializer.Deserialize<NameOtel>(stringTask);

                nameOtels.Add(result);
            }

            client.Dispose();

            return nameOtels;
        }

        public async Task<List<Hotel>> GetOtelByCountry(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/country?id=" + id);

            var result = JsonSerializer.Deserialize<List<Hotel>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<CountryOfOtel>> GetCountryData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Countries");

            var result = JsonSerializer.Deserialize<List<CountryOfOtel>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Hotel>> GetOtelData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels");

            var result = JsonSerializer.Deserialize<List<Hotel>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Room>> GetRoomData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Rooms");

            var result = JsonSerializer.Deserialize<List<Room>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<TypeRoom>> GetTypeRoomData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/TypeRooms");

            var result = JsonSerializer.Deserialize<List<TypeRoom>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<NameOtel>> GetNameOtelData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/NameOtels");

            var result = JsonSerializer.Deserialize<List<NameOtel>>(stringTask);

            client.Dispose();

            return result;
        }
    }
}
