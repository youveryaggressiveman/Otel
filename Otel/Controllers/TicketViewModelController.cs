using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class TicketViewModelController
    {
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
