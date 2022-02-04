using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    public class InputCardViewModelController
    {
        public async Task<List<Card>> GetListCardByClientId( int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Cards/client?id=" + id);

            var result = JsonSerializer.Deserialize<List<Card>>(stringTask);

            client.Dispose();

            return result;

        }

        public async Task<Card> CreateCard(Card card)
        {
            var JsonObject = JsonSerializer.Serialize<Card>(card);
            var url = "http://localhost:63262/api/Cards";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<Card>(await result.Content.ReadAsStringAsync());

            client.Dispose();

            return parsedResult;
        }
    }
}
