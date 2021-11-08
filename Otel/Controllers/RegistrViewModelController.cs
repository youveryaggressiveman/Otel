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
    public class RegistrViewModelController
    {
        public async Task<Passport> CreatePassport(Passport passport)
        {
            var JsonObject = JsonSerializer.Serialize<Passport>(passport);
            var url = "http://localhost:63262/api/Passports";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<Passport>(await result.Content.ReadAsStringAsync());

            return parsedResult;
        }

        public async Task<Client> CreateClient(Client newClient)
        {
            var JsonObject = JsonSerializer.Serialize<Client>(newClient);
            var url = "http://localhost:63262/api/Clients";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<Client>(await result.Content.ReadAsStringAsync());

            return parsedResult;
        }

        public async Task<List<CountryOfOtel>> GetCountryData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Countries");

            var result = JsonSerializer.Deserialize<List<CountryOfOtel>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Passport>> GetPassportBySerialData(string serial)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/serial?serial=" + serial);

            var result = JsonSerializer.Deserialize<List<Passport>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<List<Client>> GetClientByPhone(string phone)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/phone?phone=" + phone);

            var result = JsonSerializer.Deserialize<List<Client>>(stringTask);

            client.Dispose();

            return result;

        }
    }
}
