using Otel.Command;
using Otel.Controllers;
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
    public class AdminViewModel : BaseViewModel
    {
        #region fields

        private AdminViewModelController controller;

        private Room selectedRoomByDelete;
        private TypeRoom selectedTypeRoom;
        private Currency selectedCurrency;
        private Country selectedCountry;

        private ObservableCollection<Room> listRoom;
        private ObservableCollection<ImageOfOtel> listImage;
        private ObservableCollection<TypeRoom> listTypeRoom;
        private ObservableCollection<Currency> listCurrency;
        private ObservableCollection<Country> listCountry;

        private Room newRoom;

        private string nameOtel;
        private string nameStreet;
        private string numberStreet;
        private string description;
        private string numberOfRoom;
        private string numberOfPrice;

        #endregion

        #region properties

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        public ObservableCollection<Country> ListCountry
        {
            get => listCountry;
            set
            {
                listCountry = value;
                OnPropertyChanged(nameof(ListCountry));
            }
        }

        public Currency SelectedCurrency
        {
            get => selectedCurrency;
            set
            {
                selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }

        public TypeRoom SelectedTypeRoom
        {
            get => selectedTypeRoom;
            set
            {
                selectedTypeRoom = value;
                OnPropertyChanged(nameof(SelectedTypeRoom));
            }
        }

        public ObservableCollection<Currency> ListCurrency
        {
            get => listCurrency;
            set
            {
                listCurrency = value;
                OnPropertyChanged(nameof(ListCurrency));
            }
        }

        public ObservableCollection<TypeRoom> ListTypeRoom
        {
            get => listTypeRoom;
            set
            {
                listTypeRoom = value;
                OnPropertyChanged(nameof(ListTypeRoom));
            }
        }

        public Room SelectedRoomByDelete
        {
            get => selectedRoomByDelete;
            set
            {
                selectedRoomByDelete = value;
                OnPropertyChanged(nameof(SelectedRoomByDelete));
            }
        }

        public ObservableCollection<ImageOfOtel> ListImage
        {
            get => listImage;
            set
            {
                listImage = value;
                OnPropertyChanged(nameof(ListImage));
            }
        }

        public Room NewRoom
        {
            get => newRoom;
            set
            {
                newRoom = value;
                OnPropertyChanged(nameof(NewRoom));
            }
        }

        public string NumberOfPrice
        {
            get => numberOfPrice;
            set
            {
                numberOfPrice = value;
                OnPropertyChanged(nameof(NumberOfPrice));
            }
        }

        public string NumberOfRoom
        {
            get => numberOfRoom;
            set
            {
                numberOfRoom = value;
                OnPropertyChanged(nameof(numberOfRoom));
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

        public ObservableCollection<Room> ListRoom
        {
            get => listRoom;
            set
            {
                listRoom = value;
                OnPropertyChanged(nameof(ListRoom));
            }
        }

        public string NameStreet
        {
            get => nameStreet;
            set
            {
                nameStreet = value;
                OnPropertyChanged(nameof(NameStreet));
            }
        }

        public string NumberStreet
        {
            get => numberStreet;
            set
            {
                numberStreet = value;
                OnPropertyChanged(nameof(NumberStreet));
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

        #endregion

        #region command

        public ICommand PostNewCurrency { get; private set; }
        public ICommand PostNewTypeRoom { get; private set; }
        public ICommand PostNewCountry { get; private set; }
        public ICommand PutInNewRoom { get; private set; }
        public ICommand PutInNewOtel { get; private set; }
        public ICommand PutInNewImage { get; private set; }
        public ICommand DeleteRoom { get; private set; }

        #endregion

        public AdminViewModel()
        {
            controller = new AdminViewModelController();

            ListCountry = new ObservableCollection<Country>();
            ListCurrency = new ObservableCollection<Currency>();
            ListTypeRoom = new ObservableCollection<TypeRoom>();
            ListImage = new ObservableCollection<ImageOfOtel>();
            ListRoom = new ObservableCollection<Room>();

            PostNewCurrency = new DelegateCommand(NewCurrency);
            PostNewTypeRoom = new DelegateCommand(NewTypeRoom);
            PostNewCountry = new DelegateCommand(NewCountry);
            DeleteRoom = new DelegateCommand(DeleteSelectedRoom);
            PutInNewImage = new DelegateCommand(NewImage);
            PutInNewRoom = new DelegateCommand(LoadnewRoomList);
            PutInNewOtel = new DelegateCommand(NewOtel);

            LoadAllData();
        }

        private void NewCurrency(object obj)
        {
            AddCurrencyWindow addCurrencyWindow = new AddCurrencyWindow();
            addCurrencyWindow.ShowDialog();

            LoadAllData();
        }

        private void NewTypeRoom(object obj)
        {
            AddTypeRoomWindow addTypeRoomWindow = new AddTypeRoomWindow();
            addTypeRoomWindow.ShowDialog();

            LoadAllData();
        }

        private void NewCountry(object obj)
        {
            AddCountryWindow addCountryWindow = new AddCountryWindow();
            addCountryWindow.ShowDialog();

            LoadAllData();
        }

        private void DeleteSelectedRoom(object obj)
        {
            if (ListRoom.Count == 0)
            {
                HandyControl.Controls.MessageBox.Info("Из списка комнат нечего удалять", "Предупреждение");

                return;
            }

            if (SelectedRoomByDelete == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите комнату для удаления из списка", "Предупреждение");

                return;
            }

            var result = HandyControl.Controls.MessageBox.Ask($"Вы действительно хотите удалить комнату {SelectedRoomByDelete.Number} из заказа?",
                "Подтверждение");

            if (ListRoom.Contains(SelectedRoomByDelete) && result == MessageBoxResult.OK)
            {
                ListRoom.Remove(SelectedRoomByDelete);
            }
        }

        private void NewImage(object obj)
        {
            
        }

        private async void NewOtel(object obj)
        {
            if (SelectedCountry == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите страну для отеля", "Предупреждение");

                return;
            }

            if (NameOtel == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите название отеля", "Предупреждение");

                return;
            }

            if (NameStreet == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите название улицы отеля", "Предупреждение");

                return;
            }

            if (NumberStreet == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите номер улицы отеля", "Предупреждение");

                return;
            }

            if (Description == null)
            {
                HandyControl.Controls.MessageBox.Info("Добавьте описание для отеля", "Предупреждение");

               return;
            }

            if (ListRoom.Count == 0)
            {
                HandyControl.Controls.MessageBox.Info("Добавьте хотя бы одну комнату в список", "Предупреждение");

                return;
            }

            if (ListImage.Count == 0)
            {
                HandyControl.Controls.MessageBox.Info("Добавьте хотя бы одно изображение в альбом", "Предупреждение");

                return;
            }

            var discription = new Discription()
            {
                Name = description
            };

            var addressOfOtel = new AddressOfOtel()
            {
                Name = NameStreet,
                Number = Convert.ToInt32(NumberStreet),
                Country = SelectedCountry,        
            };

            var otel = new Hotel()
            {
                Discription = discription,
                Name = NameOtel,
                AddressOfOtel = addressOfOtel,
                Room = ListRoom,
                ImageOfOtel = ListImage
            };

            var result = HandyControl.Controls.MessageBox.Ask("Вы уверены что хотите занести отель " + NameOtel + " в базу данных", "Предупреждение");

            if (result == MessageBoxResult.OK)
            {
                await controller.CreateHotel(otel);

                HandyControl.Controls.MessageBox.Success("Отель успешно занесен в базу данных", "Информация");
            }

            if (result == MessageBoxResult.Cancel)
            {
                return;
            } 
        }

        private void LoadnewRoomList(object obj)
        {
            if (SelectedTypeRoom == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите тип комнаты", "Предупреждение");

                return;
            }

            if (NumberOfRoom == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите номер комнаты", "Предупреждение");

                return;
            }

            if (NumberOfPrice == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите цену комнаты", "Предупреждение");

                return;
            }

            if (SelectedCurrency == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите валюту для оплаты", "Предупреждение");

                return;
            }

            var price = new Price()
            {
                Number = Convert.ToInt32(NumberOfPrice),
                Currency = SelectedCurrency
            };

            newRoom = new Room()
            {
                TypeRoom = SelectedTypeRoom,
                Number = Convert.ToInt32(NumberOfRoom),
                Price = price
            };

            ListRoom.Add(newRoom);
        }

        private async void LoadAllData()
        {
            ListCurrency = new ObservableCollection<Currency>();
            ListTypeRoom = new ObservableCollection<TypeRoom>();
            ListCountry = new ObservableCollection<Country>();

            var listTypeRoom = await controller.GetAllTypeRoomList();
            var listCurrency = await controller.GetAllCurrencyList();
            var listCountry = await controller.GetAllCountryList();

            foreach (var item in listCountry)
            {
                ListCountry.Add(item);
            }

            foreach (var item in listCurrency)
            {
                ListCurrency.Add(item);
            }

            foreach (var item in listTypeRoom)
            {
                ListTypeRoom.Add(item);
            }
        }
    }
}
