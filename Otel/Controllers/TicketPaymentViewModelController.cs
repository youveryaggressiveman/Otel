﻿using Otel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{ 
    public class TicketPaymentViewModelController
    {
        public async Task<Value> GetValueByPriceId(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Values/priceId?id=" + id);

            var result = JsonSerializer.Deserialize<List<Value>>(stringTask);

            client.Dispose();

            return result[0];
        }

        public async Task<Price> GetPriceByRoomId(int id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Prices/roomId?id="+ id);

            var result = JsonSerializer.Deserialize<List<Price>>(stringTask);

            client.Dispose();

            return result[0];
        }

        public async Task<CountryOfOtel> GetCountryByOtelId(Nullable<int> id)
        {
            HttpClient client = new HttpClient();
            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Countries/otelID?id=" + id);

            var result = JsonSerializer.Deserialize<List<CountryOfOtel>>(stringTask);

            client.Dispose();

            return result[0];
        }
    }
}