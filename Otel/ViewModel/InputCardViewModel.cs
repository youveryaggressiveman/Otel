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
    public class InputCardViewModel : BaseViewModel
    {
        private InputCardViewModelController controller;

        private Card selectedCard;

        private Visibility listViewVisibility = Visibility.Collapsed;
        private Visibility textBoxAndLabelVisibility = Visibility.Visible;

        private ObservableCollection<Card> cardList;

        private string cardNumber;
        private string cardYY;
        private string cardMM;
        private string cardCVC;

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

        public ICommand InputCard { get; private set; }
        public ICommand CreateNewCard { get; private set; }
        public ICommand SelectCard { get; private set; }

        public InputCardViewModel()
        {
            controller = new InputCardViewModelController();

            CardList = new ObservableCollection<Card>();

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

        private void SelectedByPayCard(object obj)
        {

            if (SelectedCard == null)
            {
                MessageBox.Show("Выберите карту", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            CardSingltone.Card = SelectedCard;

            foreach (Window item in Application.Current.Windows)
            {
                if (item is InputCardWindow)
                {
                    item.Close();
                }

                if (item is TicketPaymentWindow)
                {
                    item.Show();
                }
            }
        }

        private void CreateCard(object obj)
        {
            TextBoxAndLabelVisibility = Visibility.Visible;
            ListViewVisibility = Visibility.Collapsed;
        }

        private async void LoadAllCard()
        {
            var newCardList = await controller.GetListCardByClientId(UserSingltone.User.ID);

            foreach (var item in newCardList)
            {
                CardList.Add(item);
            }
        }

        private async void CreateHashCode(object obj)
        {
            if (CardNumber.Length == 0)
            {
                MessageBox.Show("Введите номер карты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (CardMM.Length == 0 || CardYY.Length == 0)
            {
                MessageBox.Show("Введите срок службы карты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            if (CardCVC.Length == 0)
            {
                MessageBox.Show("Введите CVC карты", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

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

            var hashcode = await controller.CreateCard(card);

            CardSingltone.Card = hashcode;

            foreach (Window item in Application.Current.Windows)
            {
                if (item is InputCardWindow)
                {
                    item.Close();
                }

                if (item is TicketPaymentWindow)
                {
                    item.Show();
                }
            }
        }
    }
}

