using Otel.Command;
using Otel.Core;
using Otel.Core.Helper;
using Otel.Model;
using Otel.View.Windows;
using Otel.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику CheckWindow
    /// </summary>
    public class CheckViewModel : BaseViewModel
    {
        #region fields

        private readonly Hotel hotel;
        private readonly Order order;

        #endregion

        #region command

        public ICommand EndPayOrder { get; private set; }

        #endregion

        public CheckViewModel(Order order, Hotel hotel)
        {
            EndPayOrder = new DelegateCommand(ShowMainWindow);

            this.order = order;
            this.hotel = hotel;

            CreateCheckFileToPdf();
        }

        /// <summary>
        /// Метод, который открывает главное окно приложения
        /// </summary>
        /// <param name="obj"></param>
        private void ShowMainWindow(object obj)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is CheckWindow)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// Метод, заносит всю информацию о заказе, для создания чека
        /// </summary>
        /// <param name="obj"></param>
        private void CreateCheckFileToPdf()
        {
            var helper = new WordHelper(@"Resources\Files\check_template.docx");

            string roomInfo = string.Empty;
            int price = 0;
            int fullPrice = 0;

            foreach (var item in order.Room)
            {
                roomInfo += item.Number + ", " + item.TypeRoom.Name + "/n";
                if (order.DepartureDate.Year == order.ArrivalDate.Year)
                {
                    price = (price
                        + item.Price.Number) * (order.DepartureDate.DayOfYear - order.ArrivalDate.DayOfYear) / 100 * (100 - UserSingltone.User.Role.Discount);
                }

                if (order.DepartureDate.Year > order.ArrivalDate.Year)
                {
                    price = (price
                        + item.Price.Number) * (order.DepartureDate.DayOfYear + 365 - order.ArrivalDate.DayOfYear) / 100 * (100 - UserSingltone.User.Role.Discount);
                }
            }

            fullPrice = price / (100 - UserSingltone.User.Role.Discount) * 100;

            var items = new Dictionary<string, string>()
            {
                {"{full_price}", fullPrice.ToString()},
                {"{discount}", (fullPrice - price).ToString()},
                {"{prescient}", UserSingltone.User.Role.Discount.ToString() + "%"},
                { "{check_number}", order.ID.ToString() },
                { "{date}", DateTime.Now.ToString() },
                { "{name_otel}", hotel.Name },
                { "{address_of_otel}", hotel.AddressOfOtel.Country.Name + ", " + hotel.AddressOfOtel.Name + ", " + hotel.AddressOfOtel.Number },
                { "{room_info}",  roomInfo },
                { "{price}",  price.ToString() },
                { "{FIO}",  UserSingltone.User.SecondName + " " + UserSingltone.User.FirstName + " " + UserSingltone.User.LastName},
                { "{last_card_number}", "**** " + CardSingltone.Card.LastFourDigits.ToString() }
            };

            try
            {
                helper.CreateCheck(items);
            }
            catch (Exception e)
            {
                HandyControl.Controls.MessageBox.Info(
                    "Произошла ошибка при создание чека. Проверьте наличие лицензионного Word на данном устройстве",
                    "Предупреждение");
                throw;
            }
            
        }
    }
}
