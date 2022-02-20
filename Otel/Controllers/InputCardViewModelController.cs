using System;
using Otel.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    /// <summary>
    /// Класс, для работы InputCardViewModel с сервером
    /// </summary>
    public class InputCardViewModelController
    {
        /// <summary>
        /// Класс, который отправляет запрос для удаление карты на сервер  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Card> DeleteCardAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var stringTask = await client.DeleteAsync("http://localhost:63262/api/Cards/" + id);

                if (stringTask.StatusCode == HttpStatusCode.NotImplemented)
                {
                    throw new ArgumentNullException();
                }

                var result = JsonSerializer.DeserializeAsync<Card>(await stringTask.Content.ReadAsStreamAsync());

                return result.Result;
            }
        }
    }
}
