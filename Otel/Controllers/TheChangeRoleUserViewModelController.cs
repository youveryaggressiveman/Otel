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
    public class TheChangeRoleUserViewModelController
    {
        public async Task<List<User>> GetUserList()
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Users");

            var result = JsonSerializer.Deserialize<List<User>>(stringTask);

            client.Dispose();

            return result;

        }

        public async Task<List<Role>> GetRoleList()
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Roles");

            var result = JsonSerializer.Deserialize<List<Role>>(stringTask);

            client.Dispose();

            return result;

        }

        public async Task<User> RefreshUserRole(User user)
        {
            var JsonObject = JsonSerializer.Serialize<User>(user);
            var url = "http://localhost:63262/api/Users/" + user.ID;
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PutAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<User>(await result.Content.ReadAsStringAsync());

            client.Dispose();

            return parsedResult;
        }
    }
}
