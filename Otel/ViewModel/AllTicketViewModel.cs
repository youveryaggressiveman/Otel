using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
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
    public class AllTicketViewModel : BaseViewModel
    {
        private AllTicketViewModelController controller;

        private Order selectedOrder;

        private ObservableCollection<Room> roomList;
        private ObservableCollection<Order> orderList;

        private string country;
        private string nameOtel;
        private string address;
        private string statusOrder;

        private System.DateTime arrivalDate;
        private System.DateTime departureDate;

        public Order SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }

        public string StatusOrder
        {
            get => statusOrder;
            set
            {
                statusOrder = value;
                OnPropertyChanged(nameof(StatusOrder));
            }
        }

        public ObservableCollection<Order> OrderList
        {
            get => orderList;
            set
            {
                orderList = value;
                OnPropertyChanged(nameof(OrderList));
            }
        }

        public ObservableCollection<Room> RoomList
        {
            get => roomList;
            set
            {
                roomList = value;
                OnPropertyChanged(nameof(RoomList));
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

        public string NameOtel
        {
            get => nameOtel;
            set
            {
                nameOtel = value;
                OnPropertyChanged(nameof(NameOtel));
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged(nameof(Address));
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

        public ICommand InfoAboutOrder { get; private set; }

        public AllTicketViewModel()
        {
            controller = new AllTicketViewModelController();

            OrderList = new ObservableCollection<Order>();
            RoomList = new ObservableCollection<Room>();

            InfoAboutOrder = new DelegateCommand(ShowInfo);

            LoadAllTicket();
        }

        private async void ShowInfo(object obj)
        {
            if (SelectedOrder == null)
            {
                MessageBox.Show("Выберите билет", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            var hotel = await controller.GetHotelBySelectedOrderId(SelectedOrder.ID);

            var roomList = await controller.GetRoomBySelectedOrderId(SelectedOrder.ID);

            DepartureDate = SelectedOrder.DepartureDate;
            Address = hotel.AddressOfOtel.Name + ", " + hotel.AddressOfOtel.Number;
            ArrivalDate = SelectedOrder.ArrivalDate;
            NameOtel = hotel.Name;
            Country = hotel.AddressOfOtel.Country.Name;

            foreach (var item in roomList)
            {
                RoomList.Add(item);
            }
        }

        private async void LoadAllTicket()
        {
            var allOrders = await controller.GetOrderListByUserId(UserSingltone.User.ID);

            foreach (var item in allOrders)
            {
                OrderList.Add(item);
            }
        }
    }
}
