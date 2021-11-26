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
    public class AllTicketViewModelController
    {
        public async Task<Hotel> GetHotelBySelectedOrderId(int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Otels/order?id=" + id);

            var result = JsonSerializer.Deserialize<List<Hotel>>(stringTask);

            client.Dispose();

            return result[0];

        }

        public async Task<List<Order>> GetOrderListByUserId(int id)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Orders/user?id=" + id);

            var result = JsonSerializer.Deserialize<List<Order>>(stringTask);

            client.Dispose();

            return result;

        }
    }
}
