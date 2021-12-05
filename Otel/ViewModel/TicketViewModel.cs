
using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using Otel.Windows;
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

        private Visibility visibilityTheChangeRole = Visibility.Collapsed;
        private Visibility visibilityExitInAccount = Visibility.Collapsed;
        private Visibility visibilityAdminButton = Visibility.Collapsed;
        private Visibility visibilityHomeButton = Visibility.Collapsed;
        private Visibility visibilityAllTicketButton = Visibility.Visible;

        private bool isEnabledButton = false;

        public Visibility VisibilityTheChangeRole
        {
            get => visibilityTheChangeRole;
            set
            {
                visibilityTheChangeRole = value;
                OnPropertyChanged(nameof(VisibilityTheChangeRole));
            }
        }

        public Visibility VisibilityExitInAccount
        {
            get => visibilityExitInAccount;
            set
            {
                visibilityExitInAccount = value;
                OnPropertyChanged(nameof(VisibilityExitInAccount));
            }
        }

        public Visibility VisibilityAllTicketButton
        {
            get => visibilityAllTicketButton;
            set
            {
                visibilityAllTicketButton = value;
                OnPropertyChanged(nameof(VisibilityAllTicketButton));
            }
        }

        public Visibility VisibilityHomeButton
        {
            get => visibilityHomeButton;
            set
            {
                visibilityHomeButton = value;
                OnPropertyChanged(nameof(VisibilityHomeButton));
            }
        }

        public Visibility VisibilityAdminButton
        {
            get => visibilityAdminButton;
            set
            {
                visibilityAdminButton = value;
                OnPropertyChanged(nameof(VisibilityAdminButton));
            }
        }

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
                RoomList = new ObservableCollection<Room>();
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
                RoomList = new ObservableCollection<Room>();
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

        public ICommand ViewTheChangeRole { get; private set; }
        public ICommand ExitIsAccount { get; private set; }
        public ICommand ViewPageAdminMode { get; private set; }
        public ICommand ViewPageHome { get; private set; }
        public ICommand ViewPageAllTicket { get; private set; }
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

            ViewTheChangeRole = new DelegateCommand(TheChangeRole);
            ExitIsAccount = new DelegateCommand(ExitAccount);
            ViewPageAdminMode = new DelegateCommand(PageAdminMode);
            ViewPageHome = new DelegateCommand(PageHome);
            ViewPageAllTicket = new DelegateCommand(PageAllTicket);
            PreviousImage = new DelegateCommand(SetPreviousImage);
            NextImage = new DelegateCommand(SetNextImage);
            AddRoom = new DelegateCommand(AddRoomToRoomList);
            FormalizationCommand = new DelegateCommand(Formaliztion);
            DeleteRoom = new DelegateCommand(DeleteRoomFromList);

            LoadOtel();
            LoadClient();
        }

        private void ExitAccount(object obj)
        {
            UserSingltone.User = null;
            CardSingltone.Card = null;

            RoomList = new ObservableCollection<Room>();

            LoadClient();
        }

        private void TheChangeRole(object obj)
        {
            VisibilityTheChangeRole = Visibility.Collapsed;
            VisibilityAllTicketButton = Visibility.Collapsed;
            VisibilityHomeButton = Visibility.Visible;
            VisibilityAdminButton = Visibility.Visible;
        }

        private void PageAdminMode(object obj)
        {
            VisibilityAdminButton = Visibility.Collapsed;
            VisibilityAllTicketButton = Visibility.Collapsed;
            VisibilityHomeButton = Visibility.Visible;

            if (UserSingltone.User.RoleID == 3 || UserSingltone.User.RoleID == 4)
            {
                VisibilityTheChangeRole = Visibility.Visible;
            }
            
            if (UserSingltone.User.RoleID == 2)
            {
                VisibilityTheChangeRole = Visibility.Collapsed;
            }
        }

        private void PageHome(object obj)
        {
            VisibilityAllTicketButton = Visibility.Visible;
            VisibilityHomeButton = Visibility.Collapsed;

            if (UserSingltone.User.RoleID == 2)
            {
                VisibilityAdminButton = Visibility.Visible;
            }

            if (UserSingltone.User.RoleID == 3 || UserSingltone.User.RoleID == 4)
            {
                VisibilityAdminButton = Visibility.Visible;
                VisibilityTheChangeRole = Visibility.Collapsed;
            }

            if (UserSingltone.User.RoleID == 1)
            {
                VisibilityAdminButton = Visibility.Collapsed;
                VisibilityTheChangeRole = Visibility.Collapsed;
            }
        }

        private void PageAllTicket(object obj)
        {
            VisibilityTheChangeRole = Visibility.Collapsed;
            VisibilityAllTicketButton = Visibility.Collapsed;
            VisibilityAdminButton = Visibility.Collapsed;
            VisibilityHomeButton = Visibility.Visible;
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

            if (RoomNumber == null)
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
                MessageBox.Show("Из списка комнат нечего удалять", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (SelectedRoomForDelete == null)
            {
                MessageBox.Show("Выберите комнату для удаления из списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

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
                VisibilityExitInAccount = Visibility.Visible;

                IsEnabledButton = true;

                Phone = UserSingltone.User.Phone;
                FirstName = UserSingltone.User.FirstName;

                if (UserSingltone.User.RoleID == 2)
                {
                    VisibilityAdminButton = Visibility.Visible;
                }

                if (UserSingltone.User.RoleID == 3 || UserSingltone.User.RoleID == 4)
                {
                    VisibilityAdminButton = Visibility.Visible;
                    VisibilityTheChangeRole = Visibility.Collapsed;
                }

                if (UserSingltone.User.RoleID == 1)
                {
                    VisibilityAdminButton = Visibility.Collapsed;
                    VisibilityTheChangeRole = Visibility.Collapsed;
                }
            }

            if (UserSingltone.User == null)
            {
                VisibilityLabel = Visibility.Collapsed;
                VisibilityButton = Visibility.Visible;
                VisibilityAdminButton = Visibility.Collapsed;
                VisibilityHomeButton = Visibility.Collapsed;
                VisibilityTheChangeRole = Visibility.Collapsed;
                VisibilityAllTicketButton = Visibility.Visible;
                VisibilityExitInAccount = Visibility.Collapsed;

                IsEnabledButton = false;
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
                MessageBox.Show("Войдите в аккаунт", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if (item is MainWindow)
                    {
                        item.Close();
                    }
                }

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

            if (ArrivalDate == DeparatureDate)
            {
                MessageBox.Show("Дата приезда не может быть одинаковой с датой отъезда", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (ArrivalDate > DeparatureDate)
            {
                MessageBox.Show("Дата приезда не может быть позже чем дата отъезда", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if  (ArrivalDate < DateTime.Now && DeparatureDate < DateTime.Now)
            {
                MessageBox.Show("Нельзя заказывать комнату на уже прошедшее число", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if ((DeparatureDate.Year - ArrivalDate.Year) >= 2)
            {
                MessageBox.Show("Бронирование на такой большой срок невозможно", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (RoomList.Count == 0)
            {
                MessageBox.Show("Выберите номер команты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (RoomList.Count >= 2)
            {
                for (int i = 1; i < RoomList.Count; i++)
                {
                    if (RoomList[i].OtelID == RoomList[i-1].OtelID && RoomList[i].Number == RoomList[i - 1].Number)
                    {
                        MessageBox.Show("В вашем списке присутствуют две одинаковые комнаты.\nПожалуйста, оставьте только один экземпляр данной комнаты",
                           "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                        return;
                    }

                    if (RoomList[i].OtelID != RoomList[i - 1].OtelID)
                    {
                        MessageBox.Show("В списке ваших комнат присутствуют комнаты с разными отелями.\nПожалуйста, оставьте в списке комнаты только те, которые относятся к одному отелю",
                            "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                        return;
                    }
                }

            }

            SetSplash(true);

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

            foreach (Window item in Application.Current.Windows)
            {
                if (item is MainWindow)
                {
                    item.Close();
                }
            }

            SetSplash(false);
        }

        private void LoadImageByOtel()
        {

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

            if (bitmapImages.Count == 0)
            {
                ImageByOtel = null;

                return;
            }
      
            ImageByOtel = bitmapImages[0];
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
