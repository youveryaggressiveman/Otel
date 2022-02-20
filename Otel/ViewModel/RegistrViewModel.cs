using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using Otel.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику RegistrWindow
    /// </summary>
    public class RegistrViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<User> universalControllerCreateUser;
        private readonly UniversalController<Country> universalControllerCountry;
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

        #endregion

        #region properties

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

        #endregion

        #region command

        public ICommand Cancel { get; private set; }
        public ICommand RegistrCommand { get; private set; }

        #endregion

        public RegistrViewModel()
        {
            universalControllerCreateUser = new UniversalController<User>();
            universalControllerCountry = new UniversalController<Country>();
            controller = new RegistrViewModelController();

            Cancel = new DelegateCommand(CancelThisWindow);
            RegistrCommand = new DelegateCommand(Registration);

            countries = new ObservableCollection<Country>();

            LoadAllData();
        }

        /// <summary>
        /// Метод, который закрывает данное окно приложения
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Метод, который управлет видимостью окна загрузки в интерфейсе
        /// </summary>
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

        /// <summary>
        /// Метод, который производит функцию регистрации в приложении
        /// </summary>
        /// <param name="obj"></param>
        private async void Registration(object obj)
        { 
            var newUser = await controller.GetClientByPhone(Phone);

            if (string.IsNullOrWhiteSpace(PassportSerial) || string.IsNullOrWhiteSpace(PassportNumber) || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(SecondName)
                || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(Password) || SelectedCountries == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите все данные для регистрации", "Предупреждение");

                return;
            }

            if (newUser != null && newUser.Phone == Phone )
            {
                HandyControl.Controls.MessageBox.Info("Данный номер телефона уже зарезервирован в системе", "Предупреждение");

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
                HandyControl.Controls.MessageBox.Warning("Пользователь с такими данными уже зарегистрирован в системе", "Ошибка");

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

            UserSingltone.User = await universalControllerCreateUser.CreateAnother(user, "Users");

            SetSplash(false);

            if (UserSingltone.User != null)
            {
                HandyControl.Controls.MessageBox.Success(UserSingltone.User.FirstName + ", добро пожаловать!");

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

        /// <summary>
        /// Метод, который загружает список стран в интерфейсе
        /// </summary>
        private async void LoadAllData()
        {
            SetSplash(true);

            var countryList = await universalControllerCountry.GetAllInfo("Countries");

            foreach (var item in countryList)
            {
                Countries.Add(item);
            }

            SetSplash(false);
        }
    }
}
