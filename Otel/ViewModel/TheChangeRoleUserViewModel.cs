using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.Windows;
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
    public class TheChangeRoleUserViewModel : BaseViewModel
    {
        private TheChangeRoleUserViewModelController controller;

        private ObservableCollection<User> userList;
        private ObservableCollection<Role> roleList;

        private User selectedUser;
        private Role selectedRole;

        private string fio;
        private string country;
        private string numberPassport;
        private string serialPassport;
        private string phone;
        private string role;

        public Role SelectedRole
        {
            get => selectedRole;
            set
            {
                selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
            }
        }

        public ObservableCollection<Role> RoleList
        {
            get => roleList;
            set
            {
                roleList = value;
                OnPropertyChanged(nameof(RoleList));
            }
        }

        public string FIO
        {
            get => fio;
            set
            {
                fio = value;
                OnPropertyChanged(nameof(FIO));
            }
        }

        public string Role
        {
            get => role;
            set
            {
                role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string NumberPassport
        {
            get => numberPassport;
            set
            {
                numberPassport = value;
                OnPropertyChanged(nameof(NumberPassport));
            }
        }

        public string SerialPassport
        {
            get => serialPassport;
            set
            {
                serialPassport = value;
                OnPropertyChanged(nameof(SerialPassport));
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

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ObservableCollection<User> UserList
        {
            get => userList;
            set
            {
                userList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }

        public ICommand UpdateRoleBySelectedUser { get; private set; }
        public ICommand ViewSelectedUser { get; private set; }

        public TheChangeRoleUserViewModel()
        {
            controller = new TheChangeRoleUserViewModelController();

            RoleList = new ObservableCollection<Role>();
            UserList = new ObservableCollection<User>();

            UpdateRoleBySelectedUser = new DelegateCommand(UpdateRole);
            ViewSelectedUser = new DelegateCommand(SelectUser);

            LoadUsers();
        }

        private async void UpdateRole(object obj)
        {
            if (SelectedUser == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите пользователя для обновления роли", "Предупреждение");

                return;
            }

            if (SelectedRole == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите новую роль для пользователя", "Предупреждение");

                return;
            }

            var result = HandyControl.Controls.MessageBox.Ask("Вы уверены, что хотите изменить роль у выбранного пользователя?", "Предупреждение");

            if(result == MessageBoxResult.OK)
            {
                SelectedUser.RoleID = SelectedRole.ID;
                SelectedUser.Role = SelectedRole;

                var newUser = await controller.RefreshUserRole(SelectedUser);

                if (UserSingltone.User.ID == newUser.ID)
                {
                    UserSingltone.User = null;

                    UserSingltone.User = newUser;
                }

                LoadAllData();
            }

            if (result == MessageBoxResult.Cancel)
            {
                return;
            }
        }

        private void SelectUser(object obj)
        {
            if (SelectedUser == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите пользователя", "Предупреждение");

                return;
            }

            if (UserList == null)
            {
                HandyControl.Controls.MessageBox.Info("В базе данных еще нет пользователей", "Предупреждение");

                return;
            }

            FIO = SelectedUser.SecondName + " " + SelectedUser.FirstName + " " + SelectedUser.LastName;
            Country = SelectedUser.Country.Name;
            Role = SelectedUser.Role.Name;
            NumberPassport = SelectedUser.Passport.PassportNumber;
            SerialPassport = SelectedUser.Passport.PassportSerial;
            Phone = SelectedUser.Phone;

            LoadRole();
        }

        private async void LoadRole()
        {
            if (RoleList != null)
            {
                RoleList = new ObservableCollection<Role>();
            }

            var listRole = await controller.GetRoleList();

            foreach (var item in listRole)
            {
                RoleList.Add(item);
            }
        }

        private async void LoadUsers()
        {
            var listUser = await controller.GetUserList();

            foreach (var item in listUser)
            {
                UserList.Add(item);
            }
        }

        private void LoadAllData()
        {
            UserList = new ObservableCollection<User>();
            RoleList = new ObservableCollection<Role>();

            FIO = null;
            Country = null;
            Role = null;
            NumberPassport = null;
            SerialPassport = null;
            Phone = null;

            LoadRole();
            LoadUsers();  
        }
    }
}
