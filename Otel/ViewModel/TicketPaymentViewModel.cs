using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using Otel.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class TicketPaymentViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<Order> universalControllerCreateOrder;

        private ObservableCollection<Room> room;

        private readonly Hotel hotel;
        private readonly Order order;

        private string country;
        private string nameHotel;
        private string typeRoom;
        private string numberOfHotel;
        private string addressOfOtel;
        private string valueOfPrice;
        private string discount;

        private System.DateTime arrivalDate;
        private System.DateTime departureDate;

        private Visibility visibility = Visibility.Collapsed;

        private int price;

        #endregion

        #region properties

        public string Discount
        {
            get => discount;
            set
            {
                discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public ObservableCollection<Room> Room
        {
            get => room;
            set
            {
                room = value;
                OnPropertyChanged(nameof(Room));
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

        #endregion

        #region command

        public ICommand Cancel { get; private set; }
        public ICommand Pay { get; private set; }

        #endregion

        public TicketPaymentViewModel(Order order, Hotel hotel)
        {
            universalControllerCreateOrder = new UniversalController<Order>();

            this.hotel = hotel;
            this.order = order;

            Room = new ObservableCollection<Room>();

            Cancel = new DelegateCommand(CancelThisWindow);
            Pay = new DelegateCommand(PayTicket);

            LoadAllData(order, hotel);
        }

        private void CancelThisWindow(object obj)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is TicketPaymentWindow)
                {
                    item.Close();
                }
            }
        }

        private void SetSplash(bool isEnabled)
        {
            if (isEnabled)
            {
                Visibility = Visibility.Visible;
            }

            if (!isEnabled)
            {
                Visibility = Visibility.Collapsed;
            }
        }

        private void PayTicket(object obj)
        {
            if (CardSingltone.Card != null)
            {
                CreateTicket();

                HandyControl.Controls.MessageBox.Success(UserSingltone.User.FirstName + ", оплата прошла успешно. Заказ был добавлен в ваш список. Хорошего отдыха!", "Информация");

                CheckWindow checkWindow = new CheckWindow(order, hotel);
                checkWindow.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if (item is TicketPaymentWindow)
                    {
                        item.Close();
                    }
                }

                return;
            }

            if (CardSingltone.Card == null)
            {
                InputCardWindow inputCardWindow = new InputCardWindow();
                inputCardWindow.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if (item is TicketPaymentWindow)
                    {
                        item.Hide();
                    }
                }

                return;
            }
        }

        private async void CreateTicket()
        {
            SetSplash(true);

            Order newOrder = new Order()
            {
                DepartureDate = DepartureDate,
                ArrivalDate = ArrivalDate,
                ClientID = UserSingltone.User.ID,
                Room = Room,
                OtelID = hotel.ID
            };

            await universalControllerCreateOrder.CreateAnother(newOrder, "Orders");

            SetSplash(false);
        }

        private void LoadAllData(Order order, Hotel hotel)
        {
            SetSplash(true);

            NameHotel = hotel.Name;
            AddressOfHotel = hotel.AddressOfOtel.Name + ", " + hotel.AddressOfOtel.Number;
            ArrivalDate = order.ArrivalDate;
            DepartureDate = order.DepartureDate;
            Country = hotel.AddressOfOtel.Country.Name;
            Discount = Convert.ToString(UserSingltone.User.Role.Discount) + "%";

            foreach (var item in order.Room)
            {
                ValueOfPrice = item.Price.Currency.Name;
                Room.Add(item);

                if (DepartureDate.Year == ArrivalDate.Year)
                {
                    Price = (Price + item.Price.Number) * (DepartureDate.DayOfYear - ArrivalDate.DayOfYear) / 100 * (100 - UserSingltone.User.Role.Discount);
                }
                
                if (DepartureDate.Year > ArrivalDate.Year)
                {
                    Price = (Price + item.Price.Number) * (DepartureDate.DayOfYear + 365 - ArrivalDate.DayOfYear) / 100 * (100 - UserSingltone.User.Role.Discount);
                }
            }

            SetSplash(false);
        }
    }
}
