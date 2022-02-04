﻿using System;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;

namespace Otel.ViewModel
{
    public class AccountChangeViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<User> universalControllerPutUser;

        private OpenFileDialog dlg;

        private BitmapImage image;
        private User thisUser;

        private string regexFullName;
        private string fullName;

        #endregion

        #region properties

        public BitmapImage Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string RegexFullName
        {
            get => regexFullName;
            set
            {
                regexFullName = value;
                OnPropertyChanged(nameof(RegexFullName));
            }
        }

        public User ThisUser
        {
            get => thisUser;
            set
            {
                thisUser = value;
                OnPropertyChanged(nameof(ThisUser));
            }
        }

        #endregion

        #region command

        public ICommand Exit { get; private set; }
        public ICommand Put { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Add { get; private set; }

        #endregion

        public AccountChangeViewModel()
        {
            universalControllerPutUser = new UniversalController<User>();

            dlg = new OpenFileDialog();

            Put = new DelegateCommand(PutUser);
            Add = new DelegateCommand(AddAvatar);
            Delete = new DelegateCommand(DeleteAvatar);

            LoadInfo();
        }

        private async void PutUser(object obj)
        {
            var splitName = FullName.Split(' ');

            if (splitName.Length != 3)
            {
                HandyControl.Controls.MessageBox.Info("Введите все ФИО для занесения \n(Последовательность: Фамилия, Имя, Отчество)", "Предупреждение");

                return;
            }

            if (string.IsNullOrWhiteSpace(UserSingltone.User.Phone))
            {
                HandyControl.Controls.MessageBox.Info("Введите новый номер телефона", "Предупреждение");

                return;
            }

            if (string.IsNullOrWhiteSpace(UserSingltone.User.Password))
            {
                HandyControl.Controls.MessageBox.Info("Введите новый пароль", "Предупреждение");

                return;
            }

            if (Image == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите фото для своего аккаунта", "Предупреждение");

                return;
            }

            ThisUser.SecondName = splitName[0];
            ThisUser.FirstName = splitName[1];
            ThisUser.LastName = splitName[2];
            ThisUser.Avatar = ImageConBytes(Image);

            var updateResult = await universalControllerPutUser.PutAnother(ThisUser, "Users", ThisUser.ID);

            UserSingltone.User = updateResult;

            HandyControl.Controls.MessageBox.Success("Обновление пользователя прошло успешно", "Информация");

            LoadInfo();
        }

        private void AddAvatar(object obj)
        {
            dlg.FileName = ""; 
            dlg.DefaultExt = ".jpg"; 
            dlg.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"; 

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                Image = new BitmapImage( new Uri(dlg.FileName));
            }
        }

        private void DeleteAvatar(object obj)
        {
            Image = new BitmapImage(new Uri("pack://application:,,,/Otel;component/Resources/Image/Programmyi-dlya-sozdaniya-avatarok.png"));

            HandyControl.Controls.MessageBox.Success("Удаление прошло успешно!", "Информация");
        }

        private void LoadInfo()
        {
            ThisUser = UserSingltone.User;
            RegexFullName = UserSingltone.User.SecondName + " " + UserSingltone.User.FirstName[0] + "." +
                            UserSingltone.User.LastName[0] + ".";
            FullName = UserSingltone.User.SecondName + " " + UserSingltone.User.FirstName + " " +
                       UserSingltone.User.LastName;
            
            ConvertByteToImage();
        }

        private byte[] ImageConBytes(BitmapImage image)
        {
            byte[] data;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();

                return data;
            }
        }

        private void ConvertByteToImage()
        {
            if (UserSingltone.User.Avatar == null)
            {
                Image = new BitmapImage(new Uri("pack://application:,,,/Otel;component/Resources/Image/Programmyi-dlya-sozdaniya-avatarok.png"));

                return;
            }

            using (var ms = new System.IO.MemoryStream(UserSingltone.User.Avatar))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();

                Image = image;
            }
        }
    }
}