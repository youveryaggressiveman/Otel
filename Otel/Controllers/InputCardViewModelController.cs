﻿using Otel.Model;
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
    public class InputCardViewModelController
    {
        public async Task<Card> CreateCard(Card card)
        {
            var JsonObject = JsonSerializer.Serialize<Card>(card);
            var url = "http://localhost:63262/api/Cards";
            HttpClient client = new HttpClient();

            var content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await client.PostAsync(url, content);

            var parsedResult = JsonSerializer.Deserialize<Card>(await result.Content.ReadAsStringAsync());

            return parsedResult;
        }
    }
}
