using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    /// <summary>
    /// Класс, для работы RegistrViewModel с сервером
    /// </summary>
    public class RegistrViewModelController
    {
        /// <summary>
        /// Метод, который получает из сервера модель паспорта по серии и номеру
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="number"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод, который отправлет запрос серверу для получения пользователя по номеру телефона
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
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
