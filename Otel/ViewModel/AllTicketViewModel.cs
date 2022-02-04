using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class AllTicketViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<Order> universalControllerOrderListByUserID;
        private readonly UniversalController<Hotel> universalControllerHotelBySelectedOrderID;

        private Order selectedOrder;

        private ObservableCollection<Room> roomList;
        private ObservableCollection<Order> orderList;

        private string country;
        private string nameOtel;
        private string address;
        private string statusOrder;

        private System.DateTime arrivalDate;
        private System.DateTime departureDate;

        #endregion

        #region properties

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

        #endregion

        #region command

        public ICommand InfoAboutOrder { get; private set; }

        #endregion

        public AllTicketViewModel()
        {
            universalControllerOrderListByUserID = new UniversalController<Order>();
            universalControllerHotelBySelectedOrderID = new UniversalController<Hotel>();

            OrderList = new ObservableCollection<Order>();
            RoomList = new ObservableCollection<Room>();

            InfoAboutOrder = new DelegateCommand(ShowInfo);

            LoadAllTicket();
        }

        private async void ShowInfo(object obj)
        {

            if (SelectedOrder == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите билет", "Предупреждение");

                return;
            }

            if (Country != null)
            {
                Country = null;
                DepartureDate = DateTime.Now;
                Address = null;
                ArrivalDate = DateTime.Now;
                RoomList = new ObservableCollection<Room>();
                NameOtel = null;
            }

            var hotel = await universalControllerHotelBySelectedOrderID.GetInfoFromAnotherTableById("Otels", "order",
                SelectedOrder.ID);

            DepartureDate = SelectedOrder.DepartureDate;
            Address = hotel.AddressOfOtel.Name + ", " + hotel.AddressOfOtel.Number;
            ArrivalDate = SelectedOrder.ArrivalDate;
            NameOtel = hotel.Name;
            Country = hotel.AddressOfOtel.Country.Name;

            foreach (var item in SelectedOrder.Room)
            {
                RoomList.Add(item);
            }
        }

        private async void LoadAllTicket()
        {

            var allOrders = await universalControllerOrderListByUserID.GetListInfoFromAnotherTableById("Orders", "user", UserSingltone.User.ID);

            foreach (var item in allOrders)
            {
                OrderList.Add(item);
            }
        }
    }
}
