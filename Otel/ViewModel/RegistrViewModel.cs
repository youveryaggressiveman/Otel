using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
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

        public ICommand RegistrCommand { get; private set; }

        public RegistrViewModel()
        {
            controller = new RegistrViewModelController();

            RegistrCommand = new DelegateCommand(Registration);

            countries = new ObservableCollection<Country>();

            LoadAllData();
        }

        private async void Registration(object obj)
        { 
            var newUser = await controller.GetClientByPhone(Phone);

            User user;
            Passport passport;

            if (string.IsNullOrWhiteSpace(PassportSerial) || string.IsNullOrWhiteSpace(PassportNumber) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(SecondName) 
                || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(Password) || SelectedCountries == null)
            {
                MessageBox.Show("Введите все данные для регистрации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            passport = new Passport()
            {
                PassportSerial = this.PassportSerial,
                PassportNumber = this.PassportNumber
            };

            var passportResult = await controller.GetPassportByData(PassportSerial, PassportNumber);

            if (passportResult != null)
            {
                MessageBox.Show("Пользователь с такими паспортными данными уже зарегистрирован в системе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            user = new User()
            {
                SecondName = this.SecondName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Phone = this.Phone,
                Password = this.Password,
                CountryID = SelectedCountries.ID,
                Passport = passport,
                RoleID = 1,
                DiscountID = 1
            };

            UserSingltone.User = await controller.CreateClient(user);

            if (UserSingltone.User != null)
            {
                MessageBox.Show(UserSingltone.User.FirstName + ", добро пожаловать!");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();

                return;
            }
        }

        private async void LoadAllData()
        {
            var countryList = await controller.GetCountryData();

            foreach (var item in countryList)
            {
                Countries.Add(item);
            }

        }
    }
}
