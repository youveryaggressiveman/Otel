using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class UniversalController<T>
    {
        public async Task<List<T>> GetAllInfo(string name)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + name);

            var result = JsonSerializer.Deserialize <List<T>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<T> GetInfoByID(string name, int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + name + "?id" + id);

            var result = JsonSerializer.Deserialize<T>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<T> GetInfoFromAnotherTableById(string nameTable1, string nameTable2, int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + nameTable1 + "/" + nameTable2 + "?id" + id);

            var result = JsonSerializer.Deserialize<T>(stringTask);

            client.Dispose();

            return result;
        }
    }
}
