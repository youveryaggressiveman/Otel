using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using Otel.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class RegistrViewModel : BaseViewModel
    {
        private readonly RegistrViewModelController controller;

        private ObservableCollection<Country> countries;

        private Country selectedCountries;

        private string passportNumber;
        private string passportSerial;
        private string firstName;
        private string secondName;
        private string lastName;
        private string password;
        private string phone;

        private Visibility visibility = Visibility.Collapsed;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public Country SelectedCountries
        {
            get => selectedCountries;
            set
            {
                selectedCountries = value;
                OnPropertyChanged(nameof(SelectedCountries));
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

        public string SecondName
        {
            get => secondName;
            set
            {
                secondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
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

        public string PassportSerial
        {
            get => passportSerial;
            set
            {
                passportSerial = value;
                OnPropertyChanged(nameof(PassportSerial));
            }
        }

        public string PassportNumber
        {
            get => passportNumber;
            set
            {
                passportNumber = value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }

        public ObservableCollection<Country> Countries
        {
            get => countries;
            set
            {
                countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        public ICommand Cancel { get; private set; }
        public ICommand RegistrCommand { get; private set; }

        public RegistrViewModel()
        {
            controller = new RegistrViewModelController();

            Cancel = new DelegateCommand(CancelThisWindow);
            RegistrCommand = new DelegateCommand(Registration);

            countries = new ObservableCollection<Country>();

            LoadAllData();
        }

        private void CancelThisWindow(object obj)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            foreach (Window item in Application.Current.Windows)
            {
                if (item is RegistrationWindow)
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

        private async void Registration(object obj)
        { 
            var newUser = await controller.GetClientByPhone(Phone);

            if (string.IsNullOrWhiteSpace(PassportSerial) || string.IsNullOrWhiteSpace(PassportNumber) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(SecondName)
                || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(Password) || SelectedCountries == null)
            {
                MessageBox.Show("Введите все данные для регистрации", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (newUser != null && newUser.Phone == Phone )
            {
                MessageBox.Show("Данный номер телефона уже зарезервирован в системе", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            User user;
            Passport passport;

            passport = new Passport()
            {
                PassportSerial = this.PassportSerial,
                PassportNumber = this.PassportNumber
            };

            var passportResult = await controller.GetPassportByData(PassportSerial, PassportNumber);

            if (passportResult != null)
            {
                MessageBox.Show("Пользователь с такими данными уже зарегистрирован в системе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            SetSplash(true);

            user = new User()
            {
                SecondName = this.SecondName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Phone = this.Phone,
                Password = this.Password,
                Country = SelectedCountries,
                Passport = passport,
                Role = new Role() 
                { 
                    ID = 1,
                    Discount = 0,
                    Name = "Клиент"
                },
            };

            UserSingltone.User = await controller.CreateClient(user);

            SetSplash(false);

            if (UserSingltone.User != null)
            {
                MessageBox.Show(UserSingltone.User.FirstName + ", добро пожаловать!");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if (item is RegistrationWindow)
                    {
                        item.Close();
                    }
                }

                return;
            }
        }

        private async void LoadAllData()
        {
            SetSplash(true);

            var countryList = await controller.GetCountryData();

            foreach (var item in countryList)
            {
                Countries.Add(item);
            }

            SetSplash(false);
        }
    }
}
