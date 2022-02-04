using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Otel.Model;

namespace Otel.Controllers
{
    public class AccountChangeViewModelController
    {
        public async Task<User> PutUser(User user)
        {
            var JsonObject = JsonSerializer.Serialize<User>(user);

            var url = "http://localhost:63262/api/Users/" + user.ID;

            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PutAsync(url, content);

            var stringResult = await result.Content.ReadAsStreamAsync();

            var parsedResult = await JsonSerializer.DeserializeAsync<User>(stringResult);

            client.Dispose();

            return parsedResult;
        }
    }
}
