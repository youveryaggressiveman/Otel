﻿using Otel.Command;
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

        public ICommand Cancel { get; private set; }
        public ICommand AuthorizeCommand { get; private set; }

        public AuthViewModel()
        {
            controller = new AuthViewModelController();

            Cancel = new DelegateCommand(CancelThisWindow);
            AuthorizeCommand = new DelegateCommand(Authorize);
        }

        private void CancelThisWindow(object obj)
        {
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

        public async void Authorize(object obj)
        {
            if (string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Password))
            {
                HandyControl.Controls.MessageBox.Info("Введите свой телефон и пароль", "Предупреждение");

                return;
            }

            AuthorizeHelper authorizeHelper = new AuthorizeHelper();

            var authResult = await authorizeHelper.Auth(Phone, Password);

            if (authResult == true)
            {
                HandyControl.Controls.MessageBox.Success(UserSingltone.User.FirstName + ", добро пожаловать!", "Проверка");

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
