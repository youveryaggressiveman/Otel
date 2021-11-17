using Otel.Command;
using Otel.Core.Helper;
using Otel.Model;
using Otel.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class CheckViewModel : BaseViewModel
    {
        private Hotel hotel;
        private Order order;

        public ICommand EndPayOrder { get; private set; }

        public CheckViewModel(Order order, Hotel hotel)
        {
            EndPayOrder = new DelegateCommand(ShowMainWindow);

            this.order = order;
            this.hotel = hotel;

            CreateCheckFileToPdf();
        }

        private void ShowMainWindow(object obj)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
        }

        private void CreateCheckFileToPdf()
        {
            var helper = new WordHelper(@"Resources\Files\check_template.docx");


            string roomInfo = string.Empty;
            double price = 0;

            foreach (var item in order.Room)
            {
                roomInfo += item.Number + ", " + item.TypeRoom.Name + "/n";
                price += item.Price.Number;
            }

            var items = new Dictionary<string, string>()
            {
                { "{check_number}", order.ID.ToString() },
                { "{date}", DateTime.Now.ToString() },
                { "{name_otel}", hotel.Name },
                { "{address_of_otel}", hotel.AddressOfOtel.Country.Name + ", " + hotel.AddressOfOtel.Name + ", " + hotel.AddressOfOtel.Number },
                { "{room_info}",  roomInfo },
                { "{price}",  price.ToString() }
            };

            helper.CreateCheck(items);
        }
    }
}
