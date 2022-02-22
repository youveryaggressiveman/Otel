using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику AccountChangeWindow
    /// </summary>
    public class AccountChangeViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<User> universalControllerPutUser;

        private OpenFileDialog dlg;

        private Visibility visibility = Visibility.Collapsed;

        private BitmapImage image;
        private User thisUser;

        private string cardHashCode = "Карта для оплаты отсутствует";
        private string regexFullName;
        private string fullName;

        #endregion

        #region properties

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }

        public string CardHashCode
        {
            get => cardHashCode;
            set
            {
                cardHashCode = value;
                OnPropertyChanged(nameof(CardHashCode));
            }
        }

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

        public ICommand AddCard { get; private set; }
        public ICommand Put { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Add { get; private set; }

        #endregion

        public AccountChangeViewModel()
        {
            universalControllerPutUser = new UniversalController<User>();

            dlg = new OpenFileDialog();

            AddCard = new DelegateCommand(LoadCard);
            Put = new DelegateCommand(PutUser);
            Add = new DelegateCommand(AddAvatar);
            Delete = new DelegateCommand(DeleteAvatar);

            LoadInfo();
            UpdateCard();
        }

        /// <summary>
        /// Метод, который управлет видимостью окна загрузки в интерфейсе
        /// </summary>
        /// <param name="isEnabled"></param>
        public void SetSplash(bool isEnabled)
        {
            if (isEnabled)
            {
                Visibility = Visibility.Visible;
            }

            if (!isEnabled)
            {
                Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Метод, который открывает окно со списком карт
        /// </summary>
        /// <param name="obj"></param>
        private void LoadCard(object obj)
        {
            SetSplash(true);

            InputCardWindow inputCardWindow = new InputCardWindow();
            inputCardWindow.ShowDialog();

            UpdateCard();

            SetSplash(false);
        }

        /// <summary>
        /// Метод, который обновляет информацию пользователя
        /// </summary>
        /// <param name="obj"></param>
        private async void PutUser(object obj)
        {
            SetSplash(true);

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

            SetSplash(false);
        }

        /// <summary>
        /// Метод, который позволяет загрузить изображение в интерфейс 
        /// </summary>
        /// <param name="obj"></param>
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
                Image = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        /// <summary>
        /// Метод, который удаляет картинку из интерфейса
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteAvatar(object obj)
        {
            Image = new BitmapImage(new Uri("pack://application:,,,/Otel;component/Resources/Image/Programmyi-dlya-sozdaniya-avatarok.png"));

            HandyControl.Controls.MessageBox.Success("Удаление прошло успешно!", "Информация");
        }

        /// <summary>
        /// Метод, который загружает всю информацию о пользователе в интерфейс 
        /// </summary>
        private void LoadInfo()
        {
            ThisUser = UserSingltone.User;
            RegexFullName = UserSingltone.User.SecondName + " " + UserSingltone.User.FirstName[0] + "." +
                            UserSingltone.User.LastName[0] + ".";
            FullName = UserSingltone.User.SecondName + " " + UserSingltone.User.FirstName + " " +
                       UserSingltone.User.LastName;

            ConvertByteToImage();
        }

        /// <summary>
        /// Метод, который преобразует картинку в массив битов
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод, который преврщает массив битов в картинку
        /// </summary>
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

        /// <summary>
        /// Метод, который обновляет информацию о выбранной карте пользователя
        /// </summary>
        private void UpdateCard()
        {
            if (CardSingltone.Card == null)
            {
                CardHashCode = "Карта для оплаты отсутствует";

                return;
            }

            CardHashCode = "Карта для оплаты: " + "**** " + CardSingltone.Card.LastFourDigits.ToString();
        }
    }
}
