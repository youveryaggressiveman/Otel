using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    /// <summary>
    /// Класс, для работы AuthViewModel с сервером
    /// </summary>
    public class AuthViewModelController
    {
        /// <summary>
        /// Метод, который получает пользователя из сервера по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<User> GetClientByPhone(string phone)
        {
            HttpClient client = new HttpClient();

            var stringTask = await client.GetStringAsync("http://localhost:63262/api/Users/phone?phone=" + phone);

            var result = JsonSerializer.Deserialize<User>(stringTask);

            client.Dispose();

            return result;

        }
    }
}
