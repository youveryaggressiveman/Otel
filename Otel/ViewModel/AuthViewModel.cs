using Otel.Command;
using Otel.Core;
using Otel.Core.Helper;
using Otel.View.Windows;
using Otel.Windows;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику AuthWindow
    /// </summary>
    public class AuthViewModel : BaseViewModel
    {
        #region fields

        private string phone;
        private string password;

        #endregion

        #region properties

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

        #endregion

        #region command

        public ICommand Cancel { get; private set; }
        public ICommand AuthorizeCommand { get; private set; }

        #endregion

        public AuthViewModel()
        {
            Cancel = new DelegateCommand(CancelThisWindow);
            AuthorizeCommand = new DelegateCommand(Authorize);
        }

        /// <summary>
        /// Метод, который закрывает данное окно
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Метод, который производит фунцию авторизации в интерфейсе
        /// </summary>
        /// <param name="obj"></param>
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
