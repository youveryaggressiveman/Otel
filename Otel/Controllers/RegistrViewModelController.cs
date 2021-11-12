using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class RegistrViewModelController
    {
        public async Task<User> CreateClient(User newUser)
        {
            var JsonObject = JsonSerializer.Serialize<User>(newUser);
            var url = "http://localhost:63262/api/Users";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<User>(await result.Content.ReadAsStringAsync());

            return parsedResult;
        }

        public async Task<List<Country>> GetCountryData()
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Countries");

            var result = JsonSerializer.Deserialize<List<Country>>(stringTask);

            client.Dispose();

            return result;
        }

        public async Task<Passport> GetPassportByData(string serial, string number)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetAsync("http://localhost:63262/api/Passports/data?serial=" + serial + "&number=" + number);
            if (stringTask.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<Passport>(await stringTask.Content.ReadAsStringAsync());

                client.Dispose();

                return result;
            }

            return null;
        }

        public async Task<User> GetClientByPhone(string phone)
        {
            HttpClient client = new HttpClient();
            try
            {
                var stringTask = await client.GetAsync("http://localhost:63262/api/Users/phone?phone=" + phone);

                if (stringTask.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<User>(await stringTask.Content.ReadAsStringAsync());

                    client.Dispose();

                    return result;
                }
                return null;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
