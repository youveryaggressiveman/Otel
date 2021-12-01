using Otel.Command;
using Otel.Controllers;
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
    public class AdminViewModel : BaseViewModel
    {
        private AdminViewModelController controller;

        private Room selectedRoomByDelete;

        private ObservableCollection<Room> listRoom;
        private ObservableCollection<ImageOfOtel> listImage;

        private Room newRoom;

        private string country;
        private string nameOtel;
        private string typeRoom;
        private string nameStreet;
        private int numberStreet;
        private string description;
        private int numberOfRoom;
        private int numberOfPrice;
        private string currency;

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

        public string Currency
        {
            get => currency;
            set
            {
                currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public int NumberOfPrice
        {
            get => numberOfPrice;
            set
            {
                numberOfPrice = value;
                OnPropertyChanged(nameof(NumberOfPrice));
            }
        }

        public int NumberOfRoom
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

        public string TypeRoom
        {
            get => typeRoom;
            set
            {
                typeRoom = value;
                OnPropertyChanged(nameof(TypeRoom));
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

        public int NumberStreet
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

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(country));
            }
        }

        public ICommand PutInNewRoom { get; private set; }
        public ICommand PutInNewOtel { get; private set; }
        public ICommand PutInNewImage { get; private set; }
        public ICommand DeleteRoom { get; private set; }

        public AdminViewModel()
        {
            controller = new AdminViewModelController();

            ListImage = new ObservableCollection<ImageOfOtel>();
            ListRoom = new ObservableCollection<Room>();

            DeleteRoom = new DelegateCommand(DeleteSelectedRoom);
            PutInNewImage = new DelegateCommand(NewImage);
            PutInNewRoom = new DelegateCommand(LoadnewRoomList);
            PutInNewOtel = new DelegateCommand(NewOtel);
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
            if (Country == null)
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

            if (NumberStreet.ToString() == null)
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

            var country = new Country()
            {
                Name = Country
            };

            var addressOfOtel = new AddressOfOtel()
            {
                Name = NameStreet,
                Number = NumberStreet,
                Country = country           
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
            if (TypeRoom == null)
            {
                MessageBox.Show("Введите тип комнаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NumberOfRoom.ToString() == null)
            {
                MessageBox.Show("Введите номер комнаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (NumberOfPrice.ToString() == null)
            {
                MessageBox.Show("Введите цену комнаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (Currency == null)
            {
                MessageBox.Show("Введите валюту для оплаты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            var typeRoom = new TypeRoom()
            {
                Name = TypeRoom
            };

            var currency = new Currency()
            {
                Name = Currency
            };

            var price = new Price()
            {
                Number = NumberOfPrice,
                Currency = currency
            };

            newRoom = new Room()
            {
                TypeRoom = typeRoom,
                Number = NumberOfRoom,
                Price = price
            };

            ListRoom.Add(newRoom);
        }
    }
}
