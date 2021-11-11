using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class TicketPaymentViewModel : BaseViewModel
    {
        private TicketPaymentViewModelController controller;

        private ObservableCollection<RoomAndType> roomAndType;

        private string country;
        private string nameHotel;
        private string typeRoom;
        private string numberOfHotel;
        private string addressOfOtel;
        private string valueOfPrice;

        private System.DateTime arrivalDate;
        private System.DateTime departureDate;

        private int price;

        public ObservableCollection<RoomAndType> RoomAndType
        {
            get => roomAndType;
            set
            {
                roomAndType = value;
                OnPropertyChanged(nameof(RoomAndType));
            }
        }
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

        public TicketPaymentViewModel(Ticket ticket, Date date, NameOtel nameOtel, List<Room> rooms, List<TypeRoom> typeRoom, string address)
        {
            controller = new TicketPaymentViewModelController();

            RoomAndType = new ObservableCollection<RoomAndType>();

            Pay = new DelegateCommand(PayTicket);

            LoadAllData(ticket, date, nameOtel, rooms, typeRoom, address);
        }

        private async void PayTicket(object obj)
        {
            if (CardSingltone.Card != null) 
            {
                return;
            } 

            if(CardSingltone.Card == null)
            {
                InputCardWindow inputCardWindow = new InputCardWindow();
                inputCardWindow.Show();
                Application.Current.Windows[0].Hide();

                return;
            }
        }

        private async void LoadAllData(Ticket ticket, Date date, NameOtel nameOtel, List<Room> rooms, List<TypeRoom> typeRoom, string address)
        {
            Country = (await controller.GetCountryByOtelId(ticket.OtelID)).Name;
            NameHotel = nameOtel.Name;
            ArrivalDate = date.ArrivalDate;
            DepartureDate = date.CountyDate;
            AddressOfHotel = address;

            ValueOfPrice = (await controller.GetValueByPriceId(rooms[0].PriceID)).Name;

            foreach (var item in rooms)
            {
                Price += (await controller.GetPriceByRoomId(item.ID)).Number;
            }

            for (int i = 0; i < rooms.Count; i++)
            {
                RoomAndType.Add(new RoomAndType
                {
                    Room = rooms[i],
                    TypeRoom = typeRoom[i]
                });
            }

        }
    }
}
