
using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Country selectedCountry;
        private TypeRoom selectedTypeRoom;
        private Room selectedRoom;
        private Hotel selectedHotel;

        private ObservableCollection<TypeRoom> selectedRoomTypeList;

        private int imageIndex = 0;
        private int selectedHotelIndex = 0;

        private List<BitmapSource> bitmapImages;
        private ObservableCollection<Room> roomNumber;
        private ObservableCollection<Country> countryOfOtelList;
        private ObservableCollection<Hotel> hotelList;
        private ObservableCollection<Room> roomList;
        private ObservableCollection<TypeRoom> typeRoomList;

        private ImageSource imageByOtel;
        private ImageSource avatar;

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

        public ImageSource Avatar
        {
            get => avatar;
            set
            {
                avatar = value;
                OnPropertyChanged(nameof(Avatar));
            }
        }

        public Hotel SelectedHotel
        {
            get => selectedHotel;
            set
            {
                selectedHotel = value;
                OnPropertyChanged(nameof(SelectedHotel));
                LoadImageByOtel();
                LoadTypeRoom();
                LoadAddress();
                LoadDesc();
                LoadNumber();
            }
        }

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
                LoadNumber();
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

        public System.DateTime ArrivalDate
        {
            get => arrivalDate;
            set
            {
                arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
                LoadNumber();
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

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                LoadOtelByCountry();
            }
        }

        public ObservableCollection<TypeRoom> TypeRoomList
        {
            get => typeRoomList;
            set
            {
                typeRoomList = value;
                OnPropertyChanged(nameof(TypeRoomList));
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

        public ObservableCollection<Country> CountryOfOtelList
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
        public ICommand NextImage { get; private set; }
        public ICommand PreviousImage { get; private set; }

        public TicketViewModel()
        {
            controller = new TicketViewModelController();
            CountryOfOtelList = new ObservableCollection<Country>();
            HotelList = new ObservableCollection<Hotel>();
            RoomList = new ObservableCollection<Room>();
            TypeRoomList = new ObservableCollection<TypeRoom>();
            RoomNumber = new ObservableCollection<Room>();
            SelectedRoomTypeList = new ObservableCollection<TypeRoom>();

            bitmapImages = new List<BitmapSource>();

            PreviousImage = new DelegateCommand(SetPreviousImage);
            NextImage = new DelegateCommand(SetNextImage);
            AddRoom = new DelegateCommand(AddRoomToRoomList);
            FormalizationCommand = new DelegateCommand(Formaliztion);
            DeleteRoom = new DelegateCommand(DeleteRoomFromList);

            LoadOtel();

            LoadClient();

        }

        private void SetPreviousImage(object obj)
        {
            var imageCount = bitmapImages.Count - 1;

            if (imageIndex <= imageCount)
            {
                imageIndex--;
            }

            if (imageIndex < 0)
            {
                imageIndex = imageCount;
            }

            if (bitmapImages.Count == 0)
            {
                return;
            }

            ImageByOtel = bitmapImages[imageIndex];
        }

        private void SetNextImage(object obj)
        {
            var imageCount = bitmapImages.Count - 1;

            if (imageIndex <= imageCount)
            {
                imageIndex++;
            }

            if (imageIndex > imageCount)
            {
                imageIndex = 0;
            }

            if(bitmapImages.Count == 0)
            {
                return;
            }

            ImageByOtel = bitmapImages[imageIndex];
        }

        private async void LoadNumber()
        {
            RoomNumber = new ObservableCollection<Room>();

            if (SelectedHotel == null)
            {
                return;
            }

            if (SelectedTypeRoom == null)
            {
                return;
            }

            var number = await controller.GetNumerByOtel(SelectedHotel.ID, ArrivalDate, SelectedTypeRoom.ID);

            if (number == null)
            {
                return;
            }

            if (RoomNumber.Count > 0)
            {
                RoomNumber = new ObservableCollection<Room>();
            }

            for (int i = 0; i < number.Count; i++)
            {
                RoomNumber.Add(number[i]);
            }                      
        }

        private void LoadAddress()
        {
            AddressOfOtel = String.Empty;

            if (SelectedHotel == null)
            {
                return;
            }

            AddressOfOtel = SelectedHotel.AddressOfOtel.Name + ", " + SelectedHotel.AddressOfOtel.Number;
        }

        private void LoadDesc()
        {
            Description = String.Empty;

            if (SelectedHotel == null)
            {
                return;
            }

            Description = SelectedHotel.Discription.Name;
        }

        private void DeleteRoomFromList(object obj)
        {
            if (RoomList.Count == 0)
            {
                MessageBox.Show("Из списка комнат нечего удалять", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (SelectedRoomForDelete == null)
            {
                MessageBox.Show("Выберите комнату для удаления из списка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

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
            if (UserSingltone.User != null)
            {
                VisibilityLabel = Visibility.Visible;
                VisibilityButton = Visibility.Collapsed;
                IsEnabledButton = true;
                Phone = UserSingltone.User.Phone;
                FirstName = UserSingltone.User.FirstName;
            }

            if (UserSingltone.User == null)
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

        private void Formaliztion(object obj)
        {
            if (UserSingltone.User == null)
            {
                MessageBox.Show("Войдите в аккаунт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Application.Current.Windows[0].Close();

                return;
            }

            if (SelectedCountry == null)
            {
                MessageBox.Show("Выберите страну", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (SelectedHotel == null)
            {
                MessageBox.Show("Выберите отель", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (SelectedTypeRoom == null)
            {
                MessageBox.Show("Выберите тип команты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if  (ArrivalDate < DateTime.Now && DeparatureDate < DateTime.Now)
            {
                MessageBox.Show("Нельзя заказывать комнату на уже прошедшее чмло", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (ArrivalDate == DeparatureDate)
            {
                MessageBox.Show("Дата приезда не может быть одинаковой с датой отъезда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (ArrivalDate > DeparatureDate)
            {
                MessageBox.Show("Дата приезда не может быть позже чем дата отъезда", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if ((DeparatureDate.Year - ArrivalDate.Year) >= 2)
            {
                MessageBox.Show("Бронирование на такой большой срок невозможно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (RoomList.Count == 0)
            {
                MessageBox.Show("Выберите номер команты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

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

            Order newOrder = new Order()
            {
                ClientID = UserSingltone.User.ID,
                OtelID = SelectedHotel.ID,
                ArrivalDate = ArrivalDate,
                DepartureDate = DeparatureDate,
                Room = RoomList
            };

            TicketPaymentWindow ticketPayment = new TicketPaymentWindow(newOrder, SelectedHotel);
            ticketPayment.Show();
            Application.Current.Windows[0].Close();
        }

        private void LoadImageByOtel()
        {
            SetSplash(true);

            bitmapImages = new List<BitmapSource>();

            if(SelectedHotel == null)
            {
                ImageByOtel = null;

                return;
            }

            foreach (var item in SelectedHotel.ImageOfOtel)
            {
                var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(item.Image);

                bitmapImages.Add(bitmap);
            }
      
            ImageByOtel = bitmapImages[0];

            SetSplash(false);
        }

        private async void LoadOtelByCountry()
        {
            SetSplash(true);

            try
            {

                var hotelCountryList = await controller.GetOtelByCountry(SelectedCountry.ID);

                HotelList = new ObservableCollection<Hotel>();

                foreach (var item in hotelCountryList)
                {
                    HotelList.Add(item);
                }
            }
            catch (Exception ex)
            {

                var result = MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }

            SetSplash(false);
        }

        private async void LoadOtel()
        {
            SetSplash(true);

            try
            {
                var countryOfOtel = await controller.GetCountryData();

                foreach (var item in countryOfOtel)
                {
                    CountryOfOtelList.Add(item);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки данных, приложение будет закрыто", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                Application.Current.Shutdown();
            }

            SetSplash(false);
        }

        private async void LoadTypeRoom()
        {
            TypeRoomList = new ObservableCollection<TypeRoom>();

            if (SelectedHotel == null)
            {
                return;
            }

            var typeRoom = await controller.GetTypeRoomDataBySelectedOtel(SelectedHotel.ID);

            foreach (var item in typeRoom)
            {
                TypeRoomList.Add(item);
            }
        }
    }
}
