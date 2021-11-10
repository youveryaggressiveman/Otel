using Otel.Command;
using Otel.Controllers;
using Otel.Core;
using Otel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class InputCardViewModel : BaseViewModel
    {
        private InputCardViewModelController controller;

        private string cardNumber;
        private string cardYY;
        private string cardMM;
        private string cardCVC;

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

        public InputCardViewModel()
        {
            controller = new InputCardViewModelController();

            InputCard = new DelegateCommand(CreateHashCode);
        }

        private async void CreateHashCode(object obj)
        {


            var result = CardNumber.Replace(" ", "").GetHashCode() + CardCVC.GetHashCode() + CardMM.GetHashCode() + CardYY.GetHashCode();

            var card = new Card()
            {
                ClientID = ClientSingltone.Client.ID,
                HashCode = result
            };

            var hashcode = await controller.CreateCard(card);

            CardSingltone.Card = hashcode;
        }
    }
}
