using Otel.Command;
using Otel.Controllers;
using Otel.Model;
using Otel.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class AddTypeRoomViewModel : BaseViewModel
    {
        private AddTypeRoomViewModelController controller;

        private string typeRoom;

        public string TypeRoom
        {
            get => typeRoom;
            set
            {
                typeRoom = value;
                OnPropertyChanged(nameof(typeRoom));
            }
        }

        public ICommand AddNewTypeRoom { get; private set; }

        public AddTypeRoomViewModel()
        {
            controller = new AddTypeRoomViewModelController();

            AddNewTypeRoom = new DelegateCommand(NewTypeRoom);
        }

        private async void NewTypeRoom(object obj)
        {
            if (TypeRoom == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите новый тип команты", "Предупреждение");

                return;
            }

            var typeRoom = new TypeRoom()
            {
                Name = TypeRoom
            };

            var newTypeRoom = await controller.CreateTypeRoom(typeRoom);

            if (newTypeRoom.ID == 0)
            {
                HandyControl.Controls.MessageBox.Info("Такой тип команты уже существует в базе данных", "Предупреждение");

                return;
            }

            HandyControl.Controls.MessageBox.Success(newTypeRoom.Name + " тип команты добавлен список", "Предупреждение");

            foreach (Window item in Application.Current.Windows)
            {
                if (item is AddTypeRoomWindow)
                {
                    item.Close();
                }
            }
        }
    }
}
