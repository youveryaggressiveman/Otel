using Otel.Command;
using Otel.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Otel.View.Windows;
using Otel.Core;

namespace Otel.ViewModel
{
    public class AuthViewModel: BaseViewModel
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
            var selectedUser = await controller.GetClientByPhone(Phone);

            if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Введите свой телефон и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (selectedUser.Count == 0)
            {
                MessageBox.Show("Такого пользователя не существует","Предупреждение",  MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                return;
            }

            if (selectedUser.Count > 1)
            {
                MessageBox.Show("Таких пользователей несколько", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                return;
            }

            if(selectedUser[0].Password != Password)
            {
                MessageBox.Show("Введены неверные данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            ClientSingltone.Client = selectedUser[0];

            if (selectedUser[0].Password == Password)
            {
                MessageBox.Show(ClientSingltone.Client.FirstName + ", добро пожаловать!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Application.Current.Windows[0].Close();

                return;
            }
        }
    }
}
