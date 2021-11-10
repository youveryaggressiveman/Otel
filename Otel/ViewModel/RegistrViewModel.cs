using Otel.Command;
using Otel.Controllers;
using Otel.Core;
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
    public class RegistrViewModel : BaseViewModel
    {
        private readonly RegistrViewModelController controller;

        private ObservableCollection<CountryOfOtel> countries;

        private CountryOfOtel selectedCountries;

        private string passportNumber;
        private string passportSerial;
        private string firstName;
        private string secondName;
        private string lastName;
        private string password;
        private string phone;

        public CountryOfOtel SelectedCountries
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

        public ObservableCollection<CountryOfOtel> Countries
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

            countries = new ObservableCollection<CountryOfOtel>();

            LoadAllData();
        }

        private async void Registration(object obj)
        {
            var selectedPasport = await controller.GetPassportBySerialData(PassportSerial);
            var newUser = await controller.GetClientByPhone(Phone);

            Client client;
            Passport passport;

            if (string.IsNullOrWhiteSpace(PassportSerial) || string.IsNullOrWhiteSpace(PassportNumber) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(SecondName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(Password) || SelectedCountries == null)
            {
                MessageBox.Show("Введите все данные для регистрации", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (newUser.Count > 1)
            {
                MessageBox.Show("Такие данные уже существуют", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (selectedPasport.Count > 1)
            {
                MessageBox.Show("Такие данные уже существуют", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            passport = new Passport()
            {
                PassportSerial = this.PassportSerial,
                PassportNumber = this.PassportNumber
            };

            var passportResult = await controller.CreatePassport(passport);

            var passportList = await controller.GetPassportBySerialData(passportResult.PassportSerial);

            client = new Client()
            {
                SecondName = this.SecondName,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Phone = this.Phone,
                Password = this.Password,
                CountryID = SelectedCountries.ID,
                PassportID = passportList[0].ID,
                RoleID = 1
            };

            ClientSingltone.Client = await controller.CreateClient(client);

            if(ClientSingltone.Client != null)
            {
                MessageBox.Show(ClientSingltone.Client.FirstName + ", добро пожаловать!");

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
