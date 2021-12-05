using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Core.Helper;
using Otel.View.Windows;
using Otel.Windows;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly AuthViewModelController controller;

        private string phone;
        private string password;

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
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

        public ICommand AuthorizeCommand { get; private set; }

        public AuthViewModel()
        {
            controller = new AuthViewModelController();

            AuthorizeCommand = new DelegateCommand(Authorize);
        }

        public async void Authorize(object obj)
        {
            if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Введите свой телефон и пароль", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            AuthorizeHelper authorizeHelper = new AuthorizeHelper();

            var authResult = await authorizeHelper.Auth(Phone, Password);

            if (authResult == true)
            {
                MessageBox.Show(UserSingltone.User.FirstName + ", добро пожаловать!", "Проверка", MessageBoxButton.OK, MessageBoxImage.Information);

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                foreach (Window item in Application.Current.Windows)
                {
                    if (item is AuthWindow)
                    {
                        item.Close();
                    }
                }
            }
        }

        
    }
}
