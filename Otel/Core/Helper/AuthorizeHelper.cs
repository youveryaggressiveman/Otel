using Otel.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Otel.Core.Helper
{
    /// <summary>
    /// Класс, который помогает AuthViewModel при авторизации
    /// </summary>
    public class AuthorizeHelper
    {
        private readonly AuthViewModelController controller;

        public AuthorizeHelper()
        {
            controller = new AuthViewModelController();
        }

        /// <summary>
        /// Метод, еоторый определяет при введеных логина и пароля определить, существует такой аккаут или нет
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Auth(string phone, string password)
        {
            var selectedUser = await controller.GetClientByPhone(phone);

            if (selectedUser == null)
            {
                HandyControl.Controls.MessageBox.Info("Такого пользователя не существует", "Предупреждение");

                return false;
            }

            if (selectedUser.Password != password)
            {
                HandyControl.Controls.MessageBox.Info("Введены неверные данные", "Предупреждение");

                return false;
            }

            UserSingltone.User = selectedUser;

            if (selectedUser.Password == password)
            {
                return true;
            }

            return false;
        }
    }
}
