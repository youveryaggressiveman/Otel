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

        public ICommand PostNewCurrency { get; private set; }
        public ICommand PostNewTypeRoom { get; private set; }
        public ICommand PostNewCountry { get; private set; }
        public ICommand PutInNewRoom { get; private set; }
        public ICommand PutInNewOtel { get; private set; }
        public ICommand PutInNewImage { get; private set; }
        public ICommand DeleteRoom { get; private set; }

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
                MessageBox.Show("Из списка комнат нечего удалять", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (SelectedRoomByDelete == null)
            {
                MessageBox.Show("Выберите комнату для удаления из списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            var result = MessageBox.Show($"Вы действительно хотите удалить комнату {SelectedRoomByDelete.Number} из заказа?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (ListRoom.Contains(SelectedRoomByDelete) && result == MessageBoxResult.Yes)
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
                MessageBox.Show("Введите страну для отеля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NameOtel == null)
            {
                MessageBox.Show("Введите название отеля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NameStreet == null)
            {
                MessageBox.Show("Введите название улицы отеля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NumberStreet == null)
            {
                MessageBox.Show("Введите номер улицы отеля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (Description == null)
            {
               MessageBox.Show("Добавьте описание для отеля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

               return;
            }

            if (ListRoom.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну комнату в список", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (ListImage.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одно изображение в альбом", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

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

            var result = MessageBox.Show("Вы уверены что хотите занести отель " + NameOtel + " в базу данных", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                await controller.CreateHotel(otel);

                MessageBox.Show("Отель успешно занесен в базу данных", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (result == MessageBoxResult.No)
            {
                return;
            } 
        }

        private void LoadnewRoomList(object obj)
        {
            if (SelectedTypeRoom == null)
            {
                MessageBox.Show("Введите тип комнаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NumberOfRoom == null)
            {
                MessageBox.Show("Введите номер комнаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NumberOfPrice == null)
            {
                MessageBox.Show("Введите цену комнаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (SelectedCurrency == null)
            {
                MessageBox.Show("Введите валюту для оплаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

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
