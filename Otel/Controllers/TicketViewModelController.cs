using Otel.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otel.Controllers
{
    /// <summary>
    /// Класс, для работы TicketViewModel с сервером
    /// </summary>
    public class TicketViewModelController
    {
        /// <summary>
        /// Метод, который отправляет запрос на сервер для проверки доступности номера по выбранным критериям
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="typeRoomId"></param>
        /// <returns></returns>
        public async Task<List<Room>> GetNumerByOtel(int id, System.DateTime arrivalDate, int typeRoomId)
        {
            HttpClient client = new HttpClient();
            try
            {
                var stringTask = await client.GetAsync("http://localhost:63262/api/Rooms/otel?id=" + id + "&date=" + arrivalDate.ToString("yyyy-MM-dd") + "&typeRoom=" + typeRoomId);

                if (stringTask.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<List<Room>>(await stringTask.Content.ReadAsStringAsync());

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
