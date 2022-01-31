using Otel.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Otel.Core.Helper
{
    public class AuthorizeHelper
    {
        private AuthViewModelController controller;

        public AuthorizeHelper()
        {
            controller = new AuthViewModelController();
        }

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
