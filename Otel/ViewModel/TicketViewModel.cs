using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Otel.ViewModel
{
    public class TicketViewModel : BaseViewModel
    {
        private readonly TicketViewModelController controller;

        private Room selectedRoomForDelete;

        private CountryOfOtel selectedCountry;
        private TypeRoom selectedTypeRoom;
        private Room selectedRoom;
        private NameOtel selectedName;

        private ObservableCollection<TypeRoom> selectedRoomTypeList;

        private int selectedHotelIndex = 0;

        private ObservableCollection<Room> roomNumber;
        private ObservableCollection<CountryOfOtel> countryOfOtelList;
        private ObservableCollection<Hotel> hotelList;
        private ObservableCollection<Room> roomList;
        private ObservableCollection<TypeRoom> typeRoomList;
        private ObservableCollection<NameOtel> nameOtelList;

        private ImageSource imageByOtel;

        private string firstName;
        private string phone;
        private string addressOfOtel;
        private string description; 

        private System.DateTime arrivalDate = DateTime.Now;
        private System.DateTime departureDate = DateTime.Now;

        private Visibility visibility = Visibility.Collapsed;
        private Visibility visibilityLabel = Visibility.Collapsed;
        private Visibility visibilityButton = Visibility.Visible;

        private bool isEnabledButton = false;

        public ObservableCollection<TypeRoom> SelectedRoomTypeList
        {
            get => selectedRoomTypeList;
            set
            {
                selectedRoomTypeList = value;
                OnPropertyChanged(nameof(SelectedRoomTypeList));
            }
        }

        public Room SelectedRoomForDelete
        {
            get => selectedRoomForDelete;
            set
            {
                selectedRoomForDelete = value;
                OnPropertyChanged(nameof(SelectedRoomForDelete));
            }
        }
        public Room SelectedRoom
        {
            get => selectedRoom;
            set
            {
                selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public ObservableCollection<Room> RoomNumber
        {
            get => roomNumber;
            set
            {
                roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        public TypeRoom SelectedTypeRoom
        {
            get => selectedTypeRoom;
            set
            {
                selectedTypeRoom = value;
                OnPropertyChanged(nameof(SelectedTypeRoom));
                LoadRoomNumberByTypeRoom();
            }
        }

        public string AddressOfOtel
        {
            get => addressOfOtel;
            set
            {
                addressOfOtel = value;
                OnPropertyChanged(nameof(AddressOfOtel));
            }
        }

        public int SelectedHotelIndex
        {
            get => selectedHotelIndex;
            set
            {
                selectedHotelIndex = value;
                OnPropertyChanged(nameof(SelectedHotelIndex));
            }
        }

        public ImageSource ImageByOtel
        {
            get => imageByOtel;
            set
            {
                imageByOtel = value;
                OnPropertyChanged(nameof(ImageByOtel));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public NameOtel SelectedName
        {
            get => selectedName;
            set
            {
                selectedName = value;
                OnPropertyChanged(nameof(SelectedName));
                LoadDescriptionByOtel();
                LoadAddressByOtel();
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

        public System.DateTime DeparatureDate
        {
            get => departureDate;
            set
            {
                departureDate = value;
                OnPropertyChanged(nameof(DeparatureDate));
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public bool IsEnabledButton
        {
            get => isEnabledButton;
            set
            {
                isEnabledButton = value;
                OnPropertyChanged(nameof(isEnabledButton));
            }
        }

        public Visibility VisibilityButton
        {
            get => visibilityButton;
            set
            {
                visibilityButton = value;
                OnPropertyChanged(nameof(VisibilityButton));
            }
        }

        public Visibility VisibilityLabel
        {
            get => visibilityLabel;
            set
            {
                visibilityLabel = value;
                OnPropertyChanged(nameof(VisibilityLabel));
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

        public CountryOfOtel SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                LoadOtelByCountry();
            }
        }

        public ObservableCollection<NameOtel> NameOtelList
        {
            get => nameOtelList;
            set
            {
                nameOtelList = value;
                OnPropertyChanged(nameof(NameOtelList));
            }
        }

        public ObservableCollection<TypeRoom> TypeRommList
        {
            get => typeRoomList;
            set
            {
                typeRoomList = value;
                OnPropertyChanged(nameof(TypeRommList));
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

        public ObservableCollection<Hotel> HotelList
        {
            get => hotelList;
            set
            {
                hotelList = value;
                OnPropertyChanged(nameof(HotelList));
            }
        }

        public ObservableCollection<CountryOfOtel> CountryOfOtelList
        {
            get => countryOfOtelList;
            set
            {
                countryOfOtelList = value;
                OnPropertyChanged(nameof(CountryOfOtelList));
            }
        }
        public ICommand AddRoom { get; private set; }
        public ICommand FormalizationCommand { get; private set; }
        public ICommand DeleteRoom { get; private set; }

        public TicketViewModel()
        {
            controller = new TicketViewModelController();
            CountryOfOtelList = new ObservableCollection<CountryOfOtel>();
            HotelList = new ObservableCollection<Hotel>();
            RoomList = new ObservableCollection<Room>();
            TypeRommList = new ObservableCollection<TypeRoom>();
            NameOtelList = new ObservableCollection<NameOtel>();
            RoomNumber = new ObservableCollection<Room>();
            SelectedRoomTypeList = new ObservableCollection<TypeRoom>();

            AddRoom = new DelegateCommand(AddRoomToRoomList);
            FormalizationCommand = new DelegateCommand(Formaliztion);
            DeleteRoom = new DelegateCommand(DeleteRoomFromList);

            LoadAllData();

            LoadClient();

        }

        private void DeleteRoomFromList(object obj)
        {
            var result = MessageBox.Show($"Вы действительно хотите удалить комнату {SelectedRoomForDelete.Number} из заказа?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (RoomList.Contains(SelectedRoomForDelete) && result == MessageBoxResult.Yes)
            {
                RoomList.Remove(SelectedRoomForDelete);
            }
        }

        private void AddRoomToRoomList(object obj)
        {
            if (!RoomList.Contains(SelectedRoom) && SelectedRoom != null)
            {
                SelectedRoomTypeList.Add(SelectedTypeRoom);
                RoomList.Add(SelectedRoom);
            }

        }

        private void LoadClient()
        {
            if(ClientSingltone.Client != null)
            {
                VisibilityLabel = Visibility.Visible;
                VisibilityButton = Visibility.Collapsed;
                IsEnabledButton = true;
                Phone = ClientSingltone.Client.Phone;
                FirstName = ClientSingltone.Client.FirstName;
            }

            if (ClientSingltone.Client == null)
            {
                VisibilityLabel = Visibility.Collapsed;
                VisibilityButton = Visibility.Visible;
                isEnabledButton = false;
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

        private async void Formaliztion(object obj)
        {
            if(ClientSingltone.Client == null)
            {
                MessageBox.Show("Войдите в аккаунт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Application.Current.Windows[0].Close();

                return;
            }

            if (ArrivalDate > DeparatureDate)
            {
                MessageBox.Show("Дата приезда не может быть позже чем дата отъезда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if(RoomList.Count == 0)
            {
                MessageBox.Show("Выберите номер команты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            var date = new Date()
            {
                ArrivalDate = this.ArrivalDate,
                CountyDate = this.DeparatureDate
            };

            var otel = await controller.GetHotelByOtelName(SelectedName.Name);

            var ticket = new Ticket()
            {
                ClientID = ClientSingltone.Client.ID,
                OtelID = otel.ID
            };

            var rooms = new List<Room>();
            var selectedTypeRoom = new List<TypeRoom>();

            foreach (var item in RoomList)
            {
                rooms.Add(item);
            }

            foreach (var item in SelectedRoomTypeList)
            {
                selectedTypeRoom.Add(item);
            }

            TicketPaymentWindow ticketPayment = new TicketPaymentWindow(ticket, date, SelectedName, rooms, selectedTypeRoom, AddressOfOtel);
            ticketPayment.Show();
            Application.Current.Windows[0].Close();
        }

        private async void LoadImageByOtel()
        {
            SetSplash(true);

            var image = await controller.GetIamgeBySelectedOtel(SelectedName.ID);

            var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(image.Image);

            ImageByOtel = bitmap;

            SetSplash(false);
        }

        private async void LoadRoomNumberByTypeRoom()
        {
            SetSplash(true);

            RoomNumber = new ObservableCollection<Room>();

            var number = await controller.GetNumerByTypeRoomId(SelectedTypeRoom.ID);

            foreach (var item in number)
            {
                RoomNumber.Add(item);
            }

            SetSplash(false);
        }

        private async void LoadAddressByOtel()
        {
            SetSplash(true);

            AddressOfOtel = string.Empty;

            if(SelectedName == null)
            {
                return;
            }

            var address = await controller.GetAddressOfOtelBySelectedOtel(SelectedName.ID);

            AddressOfOtel = address.Name + ", " + address.Number;

            SetSplash(false);
        }

        private async void LoadDescriptionByOtel()
        {
            SetSplash(true);
                
            Description = string.Empty;

            if(SelectedName == null)
            {
                return;
            }
            var description = await controller.GetDescriptionBySelectedOtel(SelectedName.ID);

            Description = description.Name;

            LoadImageByOtel();

            SetSplash(false);
        }

        private async void LoadOtelByCountry()
        {
            SetSplash(true);

            try
            {

                var hotelCountryList = await controller.GetOtelByCountry(SelectedCountry.ID);

                var otelNameList = await controller.GetNameOtlelByOtelList(hotelCountryList);

                NameOtelList = new ObservableCollection<NameOtel>();

                foreach (var item in otelNameList)
                {
                    NameOtelList.Add(item);
                }

                SelectedName = NameOtelList[0];

            }
            catch (Exception ex)
            {

                var result = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }

            SetSplash(false);
        }

        private async void LoadAllData()
        {
            SetSplash(true);

            try
            {
                var hotel = await controller.GetOtelData();
                var countryOfOtel = await controller.GetCountryData();
                var room = await controller.GetRoomData();
                var typeRoom = await controller.GetTypeRoomData();

                foreach (var item in countryOfOtel)
                {
                    CountryOfOtelList.Add(item);
                }

                foreach (var item in hotel)
                {
                    HotelList.Add(item);
                }

                foreach (var item in typeRoom)
                {
                    TypeRommList.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки данных, приложение будет закрыто", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }

            SetSplash(false);
        }
    }
}
