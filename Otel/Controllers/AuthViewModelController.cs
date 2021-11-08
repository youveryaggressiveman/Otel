using Otel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class AuthViewModelController
    {
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
