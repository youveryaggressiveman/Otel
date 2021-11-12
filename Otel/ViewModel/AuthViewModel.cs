using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly AuthViewModelController controller;

        private string phone = "89184293165";
        private string password = "123";

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
            var selectedUser = await controller.GetClientByPhone(Phone);

            if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Введите свой телефон и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (selectedUser == null)
            {
                MessageBox.Show("Такого пользователя не существует", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                return;
            }

            if (selectedUser.Password != Password)
            {
                MessageBox.Show("Введены неверные данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            UserSingltone.User = selectedUser;

            if (selectedUser.Password == Password)
            {
                MessageBox.Show(UserSingltone.User.FirstName + ", добро пожаловать!","Проверка", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();

                return;
            }
        }
    }
}
