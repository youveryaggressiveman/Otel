using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class TicketViewModelController
    {
        public async Task<List<Room>> GetNumerByOtel(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Rooms/otel?id=" + id);

            var result = JsonSerializer.Deserialize<List<Room>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Hotel>> GetOtelByCountry(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/country?id=" + id);

            var result = JsonSerializer.Deserialize<List<Hotel>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Country>> GetCountryData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Countries");

            var result = JsonSerializer.Deserialize<List<Country>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<TypeRoom>> GetTypeRoomDataBySelectedOtel(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/TypeRooms/otel?id=" + id);

            var result = JsonSerializer.Deserialize<List<TypeRoom>>(stringTask);

            client.Dispose();

            return result;
        }

    }
}
