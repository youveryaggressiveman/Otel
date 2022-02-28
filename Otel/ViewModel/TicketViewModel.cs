﻿
using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using Otel.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Otel.Core.Services;
using Otel.Core.Utils;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику TicketWindow
    /// </summary>
    public class TicketViewModel : BaseViewModel
    {
        #region fields

        private readonly IImageService imageService;

        private readonly UniversalController<TypeRoom> universalControllerTypeRoomListByOtelID;
        private readonly UniversalController<Hotel> universalControllerOtelListByCountryID;
        private readonly UniversalController<Country> universalControllerCountry;
        private readonly TicketViewModelController controller;

        private Room selectedRoomForDelete;

        private Country selectedCountry;
        private TypeRoom selectedTypeRoom;
        private Room selectedRoom;
        private Hotel selectedHotel;

        private ObservableCollection<TypeRoom> selectedRoomTypeList;

        private int imageIndex = 0;
        private int selectedHotelIndex = 0;

        public List<BitmapSource> bitmapImages;
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
        private Visibility visibilityHomeButton = Visibility.Visible;
        private Visibility visibilityAllTicketButton = Visibility.Visible;

        private bool isEnabledButton = false;
        #endregion

        #region properties
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
        #endregion

        #region command
        public ICommand Authorization { get; private set; }
        public ICommand Registration { get; private set; }
        public ICommand ViewTheChangeRole { get; private set; }
        public ICommand ExitIsAccount { get; private set; }
        public ICommand ViewPageAdminMode { get; private set; }
        public ICommand ViewPageHome { get; private set; }
        public ICommand ViewPageAllTicket { get; private set; }
        public ICommand AddRoom { get; private set; }
        public ICommand FormalizationCommand { get; private set; }
        public ICommand DeleteRoom { get; private set; }
        public ICommand ShowProfile { get; private set; }
        #endregion

        public TicketViewModel()
        {
            imageService = new ImageService();

            universalControllerTypeRoomListByOtelID = new UniversalController<TypeRoom>();
            universalControllerOtelListByCountryID = new UniversalController<Hotel>();
            universalControllerCountry = new UniversalController<Country>();
            controller = new TicketViewModelController();

            CountryOfOtelList = new ObservableCollection<Country>();
            HotelList = new ObservableCollection<Hotel>();
            RoomList = new ObservableCollection<Room>();
            TypeRoomList = new ObservableCollection<TypeRoom>();
            RoomNumber = new ObservableCollection<Room>();
            SelectedRoomTypeList = new ObservableCollection<TypeRoom>();

            bitmapImages = new List<BitmapSource>();

            ShowProfile = new DelegateCommand(Profile);
            Authorization = new DelegateCommand(Auth);
            Registration = new DelegateCommand(Registr);
            ViewTheChangeRole = new DelegateCommand(TheChangeRole);
            ExitIsAccount = new DelegateCommand(ExitAccount);
            ViewPageAdminMode = new DelegateCommand(PageAdminMode);
            ViewPageHome = new DelegateCommand(PageHome);
            ViewPageAllTicket = new DelegateCommand(PageAllTicket);
            AddRoom = new DelegateCommand(AddRoomToRoomList);
            FormalizationCommand = new DelegateCommand(Formaliztion);
            DeleteRoom = new DelegateCommand(DeleteRoomFromList);

            LoadOtel();
            LoadClient();
        }

        /// <summary>
        /// Метод, который открывет диалоговое окно профиля пользователя
        /// </summary>
        /// <param name="obj"></param>
        private void Profile(object obj)
        {
            SetSplash(true);

            AccountChangeWindow accountChangeWindow = new AccountChangeWindow();

            accountChangeWindow.ShowDialog();

            UpdateInfo();

            SetSplash(false);

        }

        /// <summary>
        /// Метод, который открывает окно авторизации 
        /// </summary>
        /// <param name="obj"></param>
        private void Auth(object obj)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is MainWindow)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// Метод, который открывает окно регистрации
        /// </summary>
        /// <param name="obj"></param>
        private void Registr(object obj)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is MainWindow)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// Метод, который реализует функцию выхода из аккаунта
        /// </summary>
        /// <param name="obj"></param>
        private void ExitAccount(object obj)
        {
            UserSingltone.User = null;
            CardSingltone.Card = null;

            RoomList = new ObservableCollection<Room>();

            LoadClient();
        }

        /// <summary>
        /// Метод, который открывает страницу TheChangeRole
        /// </summary>
        /// <param name="obj"></param>
        private void TheChangeRole(object obj)
        {
            LoadClient();
        }

        /// <summary>
        /// Метод, который открывает страницу PageAdminMode
        /// </summary>
        /// <param name="obj"></param>
        private void PageAdminMode(object obj)
        {
            LoadClient();
        }

        /// <summary>
        /// Метод, который открывает страницу PageHome
        /// </summary>
        /// <param name="obj"></param>
        private void PageHome(object obj)
        {
            LoadClient();
        }

        /// <summary>
        /// Метод, который открывает страницу PageAllTicket
        /// </summary>
        /// <param name="obj"></param>
        private void PageAllTicket(object obj)
        {
            LoadClient();
        }

        /// <summary>
        /// Метод, который загружет номера выбранного отеля в интерфейс
        /// </summary>
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

        /// <summary>
        /// Метод, который загружает адрес отеля в интерфейс
        /// </summary>
        private void LoadAddress()
        {

            AddressOfOtel = String.Empty;

            if (SelectedHotel == null)
            {
                return;
            }

            AddressOfOtel = SelectedHotel.AddressOfOtel.Name + ", " + SelectedHotel.AddressOfOtel.Number;
        }

        /// <summary>
        /// Метод, который загружает описание отеля в интерфейс
        /// </summary>
        private void LoadDesc()
        {
            Description = String.Empty;

            if (SelectedHotel == null)
            {
                return;
            }

            Description = SelectedHotel.Discription.Name;
        }

        /// <summary>
        /// Метод, который позволяет удалить команту из списка заказа
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteRoomFromList(object obj)
        {
            if (RoomList.Count == 0)
            {
                HandyControl.Controls.MessageBox.Info("Из списка комнат нечего удалять", "Предупреждение");

                return;
            }

            if (SelectedRoomForDelete == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите комнату для удаления из списка", "Предупреждение");

                return;
            }

            var result = HandyControl.Controls.MessageBox.Ask($"Вы действительно хотите удалить комнату {SelectedRoomForDelete.Number} из заказа?",
                "Подтверждение");

            if (RoomList.Contains(SelectedRoomForDelete) && result == MessageBoxResult.Yes)
            {
                RoomList.Remove(SelectedRoomForDelete);
            }
        }

        /// <summary>
        /// Метод, который позволяет добавить команту в список заказа
        /// </summary>
        /// <param name="obj"></param>
        private void AddRoomToRoomList(object obj)
        {
            if (!RoomList.Contains(SelectedRoom) && SelectedRoom != null)
            {
                SelectedRoomTypeList.Add(SelectedTypeRoom);
                RoomList.Add(SelectedRoom);
            }

        }

        /// <summary>
        /// Метод, который загружает информацию о пользователе 
        /// </summary>
        public void LoadClient()
        {
            if (UserSingltone.User != null)
            {
                VisibilityLabel = Visibility.Visible;
                VisibilityButton = Visibility.Collapsed;
                VisibilityExitInAccount = Visibility.Visible;

                IsEnabledButton = true;

                ConvertByteToImage();

                Phone = UserSingltone.User.Phone;
                FirstName = UserSingltone.User.FirstName;

                if (UserSingltone.User.RoleID == 2)
                {
                    VisibilityAdminButton = Visibility.Visible;
                    VisibilityTheChangeRole = Visibility.Collapsed;
                }

                if (UserSingltone.User.RoleID == 3 || UserSingltone.User.RoleID == 4)
                {
                    VisibilityAdminButton = Visibility.Visible;
                    VisibilityTheChangeRole = Visibility.Visible;
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
                VisibilityHomeButton = Visibility.Visible;
                VisibilityTheChangeRole = Visibility.Collapsed;
                VisibilityAllTicketButton = Visibility.Visible;
                VisibilityExitInAccount = Visibility.Collapsed;

                IsEnabledButton = false;
            }
        }

        /// <summary>
        /// Метод, который управлет видимостью окна загрузки в интерфейсе
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetSplash(bool isEnabled)
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

        /// <summary>
        /// Метод, который позволяет создать новый заказ 
        /// </summary>
        /// <param name="obj"></param>
        private void Formaliztion(object obj)
        {
            if (UserSingltone.User == null)
            {
                HandyControl.Controls.MessageBox.Info("Войдите в аккаунт", "Предупреждение");

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
                HandyControl.Controls.MessageBox.Info("Выберите страну", "Предупреждение");

                return;
            }

            if (SelectedHotel == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите отель", "Предупреждение");

                return;
            }

            if (SelectedTypeRoom == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите тип команты", "Предупреждение");

                return;
            }

            if (ArrivalDate == DeparatureDate)
            {
                HandyControl.Controls.MessageBox.Info("Дата приезда не может быть одинаковой с датой отъезда", "Предупреждение");

                return;
            }

            if (ArrivalDate > DeparatureDate)
            {
                HandyControl.Controls.MessageBox.Info("Дата приезда не может быть позже чем дата отъезда", "Предупреждение");

                return;
            }

            if (ArrivalDate < DateTime.Now && DeparatureDate < DateTime.Now)
            {
                HandyControl.Controls.MessageBox.Info("Нельзя заказывать комнату на уже прошедшее число", "Предупреждение");

                return;
            }

            if ((DeparatureDate.Year - ArrivalDate.Year) >= 2)
            {
                HandyControl.Controls.MessageBox.Info("Бронирование на такой большой срок невозможно", "Предупреждение");

                return;
            }

            if (RoomList.Count == 0)
            {
                HandyControl.Controls.MessageBox.Info("Выберите номер команты", "Предупреждение");

                return;
            }

            if (RoomList.Count >= 2)
            {
                for (int i = 1; i < RoomList.Count; i++)
                {
                    if (RoomList[i].OtelID == RoomList[i - 1].OtelID && RoomList[i].Number == RoomList[i - 1].Number)
                    {
                        HandyControl.Controls.MessageBox.Info("В вашем списке присутствуют две одинаковые комнаты.\nПожалуйста, оставьте только один экземпляр данной комнаты",
                           "Предупреждение");

                        return;
                    }

                    if (RoomList[i].OtelID != RoomList[i - 1].OtelID)
                    {
                        HandyControl.Controls.MessageBox.Info("В списке ваших комнат присутствуют комнаты с разными отелями.\nПожалуйста, оставьте в списке комнаты только те, которые относятся к одному отелю",
                            "Предупреждение");

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

        /// <summary>
        /// Метод, который загружает список изображения отеля в интерфейс
        /// </summary>
        private void LoadImageByOtel()
        {

            bitmapImages = new List<BitmapSource>();

            if (SelectedHotel == null)
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

            SaveImages();

            ImageByOtel = bitmapImages[0];
        }

        /// <summary>
        /// Метод, который загружает в интерфейс список отелей в зависимости от страны 
        /// </summary>
        private async void LoadOtelByCountry()
        {
            SetSplash(true);

            try
            {

                var hotelCountryList = await universalControllerOtelListByCountryID.GetListInfoFromAnotherTableById("Otels", "country", SelectedCountry.ID);

                HotelList = new ObservableCollection<Hotel>();

                foreach (var item in hotelCountryList)
                {
                    HotelList.Add(item);
                }
            }
            catch (Exception ex)
            {
                var result = HandyControl.Controls.MessageBox.Error(ex.Message, "Error");

                Application.Current.Shutdown();
            }

            SetSplash(false);
        }

        /// <summary>
        /// Метод, который загружает список стран в интерфейс
        /// </summary>
        private async void LoadOtel()
        {
            SetSplash(true);

            try
            {
                var countryOfOtel = await universalControllerCountry.GetAllInfo("Countries");

                foreach (var item in countryOfOtel)
                {
                    CountryOfOtelList.Add(item);
                }

            }
            catch (Exception)
            {
                HandyControl.Controls.MessageBox.Error("Ошибка загрузки данных, приложение будет закрыто", "Error");

                Application.Current.Shutdown();
            }

            SetSplash(false);
        }

        /// <summary>
        /// Сетод, котрый загружает список типов комнат в интерфейс
        /// </summary>
        private async void LoadTypeRoom()
        {
            TypeRoomList = new ObservableCollection<TypeRoom>();

            if (SelectedHotel == null)
            {
                return;
            }

            var typeRoom = await universalControllerTypeRoomListByOtelID.GetListInfoFromAnotherTableById("TypeRooms", "otel", SelectedHotel.ID);

            foreach (var item in typeRoom)
            {
                TypeRoomList.Add(item);
            }
        }

        /// <summary>
        /// Метод, который превращает массив битов в изображение
        /// </summary>
        private void ConvertByteToImage()
        {
            if (UserSingltone.User.Avatar == null)
            {
                Avatar = new BitmapImage(new Uri("pack://application:,,,/Otel;component/Resources/Image/Programmyi-dlya-sozdaniya-avatarok.png"));

                return;
            }

            var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(UserSingltone.User.Avatar);

            Avatar = bitmap;
        }

        /// <summary>
        /// Метод, который сохраняет картинки в файлы ресурсов
        /// </summary>
        private async void SaveImages()
        {
            await imageService.Save(bitmapImages, SelectedHotel.Name);
        }

        /// <summary>
        /// Метод, который обновляет информацию о пользователе
        /// </summary>
        private void UpdateInfo()
        {
            Avatar = null;
            Phone = null;
            FirstName = null;

            LoadClient();
        }
    }
}
