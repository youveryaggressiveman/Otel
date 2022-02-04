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
    public class UniversalController<T>
    {
        public async Task<List<T>> GetAllInfo(string name)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + name);

            var result = JsonSerializer.Deserialize<List<T>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<T> GetInfoById(string name, int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + name + "?id=" + id);

            var result = JsonSerializer.Deserialize<T>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<T>> GetListInfoFromAnotherTableById(string nameTable1, string nameTable2, int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + nameTable1 + "/" + nameTable2 + "?id=" + id);

            var result = JsonSerializer.Deserialize<List<T>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<T> GetInfoFromAnotherTableById(string nameTable1, string nameTable2, int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/" + nameTable1 + "/" + nameTable2 + "?id=" + id);

            var result = JsonSerializer.Deserialize<T>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<T> CreateAnother<T>(T another, string name)
        {
            var jsonObject = JsonSerializer.Serialize<T>(another);
            string url = "http://localhost:63262/api/" + name;

            HttpClient client = new HttpClient();

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var stringTask = await client.PostAsync(url, content);

            var parsedStringTask = await stringTask.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<T>(parsedStringTask);

            client.Dispose();

            return result;
        }

        public async Task<T> PutAnother<T>(T another, string name, int id)
        {
            var jsonObject = JsonSerializer.Serialize<T>(another);
            string url = "http://localhost:63262/api/" + name + "/" + id;

            HttpClient client = new HttpClient();

            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var stringTask = await client.PutAsync(url, content);

            var parsedStringTask = await stringTask.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<T>(parsedStringTask);

            client.Dispose();

            return result;
        }
    }
}
