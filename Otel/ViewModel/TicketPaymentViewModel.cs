using Otel.Command;
using Otel.Controllers;
using Otel.Model;
using Otel.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class TicketPaymentViewModel : BaseViewModel
    {
        private TicketViewModelController ticketController;
        private TicketPaymentViewModelController controller;
        private string country;
        private string nameHotel;
        private string typeRoom;
        private string numberOfHotel;
        private string addressOfOtel;
        private System.DateTime arrivalDate;
        private System.DateTime departureDate;
        private int price;
        private string valueOfPrice;

        public string ValueOfPrice
        {
            get => valueOfPrice;
            set
            {
                valueOfPrice = value;
                OnPropertyChanged(nameof(ValueOfPrice));
            }
        }

        public string AddressOfHotel
        {
            get => addressOfOtel;
            set
            {
                addressOfOtel = value;
                OnPropertyChanged(nameof(AddressOfHotel));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string NameHotel
        {
            get => nameHotel;
            set
            {
                nameHotel = value;
                OnPropertyChanged(nameof(NameHotel));
            }
        }

        public string TypeRoom
        {
            get => typeRoom;
            set
            {
                typeRoom = value;
                OnPropertyChanged(nameof(TypeRoom));
            }
        }

        public string NumberOfHotel
        {
            get => numberOfHotel;
            set
            {
                numberOfHotel = value;
                OnPropertyChanged(nameof(NumberOfHotel));
            }
        }

        public System.DateTime ArrivalDate
        {
            get => arrivalDate;
            set
            {
                arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
            }
        }

        public System.DateTime DepartureDate
        {
            get => departureDate;
            set
            {
                departureDate = value;
                OnPropertyChanged(nameof(DepartureDate));
            }
        }

        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ICommand Pay { get; private set; }

        public TicketPaymentViewModel(Ticket ticket, Date date, NameOtel nameOtel, List<Room> rooms, TypeRoom typeRoom, string address)
        {
            ticketController = new TicketViewModelController();
            controller = new TicketPaymentViewModelController();

            Pay = new DelegateCommand(PayTicket);

            LoadAllData(ticket, date, nameOtel, rooms, typeRoom, address);
        }

        private void PayTicket(object obj)
        {


            InputCardWindow inputCardWindow = new InputCardWindow();
            inputCardWindow.Show();
        }

        private async void LoadAllData(Ticket ticket, Date date, NameOtel nameOtel, List<Room> rooms, TypeRoom typeRoom, string address)
        {
            Country = (await controller.GetCountryByOtelId(ticket.OtelID)).Name;
            NameHotel = nameOtel.Name;
            ArrivalDate = date.ArrivalDate;
            DepartureDate = date.CountyDate;
            AddressOfHotel = address;
            TypeRoom = typeRoom.Name;

            ValueOfPrice = (await controller.GetValueByPriceId(rooms[0].PriceID)).Name;

            foreach (var item in rooms)
            {
                Price += (await controller.GetPriceByRoomId(item.ID)).Number;
            }

            foreach (var item in rooms)
            {
                NumberOfHotel = item.Number + ";"; 
            }
        }
    }
}
