using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using Otel.View.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику InputCardWindow
    /// </summary>
    public class InputCardViewModel : BaseViewModel
    {
        #region fields

        private readonly InputCardViewModelController controller;
        private readonly UniversalController<Card> universalControllerCreateCard;
        private readonly UniversalController<Card> universalControllerCardListByUserID;

        private Card selectedCard;

        private Visibility listViewVisibility = Visibility.Collapsed;
        private Visibility textBoxAndLabelVisibility = Visibility.Visible;

        private ObservableCollection<Card> cardList;

        private string cardNumber;
        private string cardYY;
        private string cardMM;
        private string cardCVC;

        #endregion

        #region properties

        public Card SelectedCard
        {
            get => selectedCard;
            set
            {
                selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
            }
        }

        public Visibility TextBoxAndLabelVisibility
        {
            get => textBoxAndLabelVisibility;
            set
            {
                textBoxAndLabelVisibility = value;
                OnPropertyChanged(nameof(TextBoxAndLabelVisibility));
            }
        }

        public ObservableCollection<Card> CardList
        {
            get => cardList;
            set
            {
                cardList = value;
                OnPropertyChanged(nameof(CardList));
            }
        }

        public Visibility ListViewVisibility
        {
            get => listViewVisibility;
            set
            {
                listViewVisibility = value;
                OnPropertyChanged(nameof(ListViewVisibility));
            }
        }

        public string CardYY
        {
            get => cardYY;
            set
            {
                cardYY = value;
                OnPropertyChanged(nameof(CardYY));
            }
        }

        public string CardMM
        {
            get => cardMM;
            set
            {
                cardMM = value;
                OnPropertyChanged(nameof(CardMM));
            }
        }

        public string CardCVC
        {
            get => cardCVC;
            set
            {
                cardCVC = value;
                OnPropertyChanged(nameof(CardCVC));
            }
        }

        public string CardNumber
        {
            get => cardNumber;
            set
            {
                cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }

        #endregion

        #region command

        public ICommand DeleteCard { get; private set; }
        public ICommand Cancel { get; private set; }
        public ICommand InputCard { get; private set; }
        public ICommand CreateNewCard { get; private set; }
        public ICommand SelectCard { get; private set; }

        #endregion

        public InputCardViewModel()
        {
            controller = new InputCardViewModelController();
            universalControllerCreateCard = new UniversalController<Card>();
            universalControllerCardListByUserID = new UniversalController<Card>();

            CardList = new ObservableCollection<Card>();

            DeleteCard = new DelegateCommand(Delete);
            Cancel = new DelegateCommand(CancelThisWindow);
            InputCard = new DelegateCommand(CreateHashCode);
            SelectCard = new DelegateCommand(SelectedByPayCard);
            CreateNewCard = new DelegateCommand(CreateCard);

            if (UserSingltone.User.Card == null)
            {
                TextBoxAndLabelVisibility = Visibility.Visible;
                ListViewVisibility = Visibility.Collapsed;

                return;
            }

            if (UserSingltone.User.Card.Count == 0)
            {
                TextBoxAndLabelVisibility = Visibility.Visible;
                ListViewVisibility = Visibility.Collapsed;
            }

            if (UserSingltone.User.Card.Count > 0)
            {
                LoadAllCard();

                TextBoxAndLabelVisibility = Visibility.Collapsed;
                ListViewVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Метод, который удаляет карту из интерфейса
        /// </summary>
        /// <param name="obj"></param>
        private async void Delete(object obj)
        {
            Card card;

            try
            {
                card = await controller.DeleteCardAsync(SelectedCard.ID);

            }
            catch (Exception e)
            {
                HandyControl.Controls.MessageBox.Info("Произошла ошибка при удалении карты. Поворите попытку позже",
                    "Информация");
                return; ;
            }

            if (CardSingltone.Card?.ID == card.ID)
            {
                CardSingltone.Card = null;
            }

            HandyControl.Controls.MessageBox.Info($"Карта **** {card.LastFourDigits} была успешна удалена из системы.",
                "Информация");

            UpdateCard();
        }

        /// <summary>
        /// Метод, который закрывает данное окно приложения
        /// </summary>
        /// <param name="obj"></param>
        private void CancelThisWindow(object obj)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is InputCardWindow)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// Метод, который заносит выбранную карту, для дальнейшей работе в приложение
        /// </summary>
        /// <param name="obj"></param>
        private void SelectedByPayCard(object obj)
        {

            if (SelectedCard == null)
            {
                HandyControl.Controls.MessageBox.Info("Выберите карту", "Предупреждение");

                return;
            }

            CardSingltone.Card = SelectedCard;

            foreach (Window item in Application.Current.Windows)
            {
                if (item is InputCardWindow)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// Метод, который отбражает поля для создания новой карты
        /// </summary>
        /// <param name="obj"></param>
        private void CreateCard(object obj)
        {
            TextBoxAndLabelVisibility = Visibility.Visible;
            ListViewVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Метод, который загружает список карт пользователя в интерфейс
        /// </summary>
        private async void LoadAllCard()
        {
            var newCardList = await universalControllerCardListByUserID.GetListInfoFromAnotherTableById("Cards", "client", UserSingltone.User.ID);

            foreach (var item in newCardList)
            {
                CardList.Add(item);
            }
        }

        /// <summary>
        /// Метод, который создает новую карту пользователя
        /// </summary>
        /// <param name="obj"></param>
        private async void CreateHashCode(object obj)
        {
            if (CardNumber.Length == 0)
            {
                HandyControl.Controls.MessageBox.Info("Введите номер карты", "Предупреждение");

                return;
            }

            if (CardMM.Length == 0 || CardYY.Length == 0)
            {
                HandyControl.Controls.MessageBox.Info("Введите срок службы карты", "Предупреждение");

                return;
            }

            if (CardCVC.Length == 0)
            {
                HandyControl.Controls.MessageBox.Info("Введите CVC карты", "Предупреждение");

                return;
            }

            var result = CardNumber.Replace(" ", "").GetHashCode() + CardCVC.GetHashCode() + CardMM.GetHashCode() + CardYY.GetHashCode();

            string lastFourDigits = string.Empty;

            for (int i = 11; i < 15; i++)
            {
                lastFourDigits += CardNumber.Replace(" ", "")[i];
            }

            var card = new Card()
            {
                ClientID = UserSingltone.User.ID,
                HashCode = result,
                LastFourDigits = Convert.ToInt32(lastFourDigits)
            };

            var hashcode = await universalControllerCreateCard.CreateAnother(card, "Cards");

            CardSingltone.Card = hashcode;

            foreach (Window item in Application.Current.Windows)
            {
                if (item is InputCardWindow)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// Метод, для обновления списка карт пользователя
        /// </summary>
        private void UpdateCard()
        {
            CardList = new ObservableCollection<Card>();

            LoadAllCard();
        }
    }
}

