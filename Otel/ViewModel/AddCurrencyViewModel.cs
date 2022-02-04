using Otel.Command;
using Otel.Controllers;
using Otel.Model;
using Otel.View.Windows;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    public class AddCurrencyViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<Currency> universalControllerCreateCurrency;

        private string currency;

        #endregion

        #region properties

        public string Currency
        {
            get => currency;
            set
            {
                currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        #endregion

        #region command

        public ICommand AddNewCurrency { get; private set; }

        #endregion

        public AddCurrencyViewModel()
        {
            universalControllerCreateCurrency = new UniversalController<Currency>();

            AddNewCurrency = new DelegateCommand(NewCurrency);
        }

        private async void NewCurrency(object obj)
        {
            if (Currency == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите новую валюту", "Предупреждение");

                return;
            }

            var currency = new Currency()
            {
                Name = Currency,
            };

            var newCurrency = await universalControllerCreateCurrency.CreateAnother(currency, "Currencies");

            if (newCurrency.ID == 0)
            {
                HandyControl.Controls.MessageBox.Info("Такая валюта уже есть в базе данных", "Предупреждение");

                return;
            }

            HandyControl.Controls.MessageBox.Success(newCurrency.Name + " добавлен в список валют", "Предупреждение");

            foreach (Window item in Application.Current.Windows)
            {
                if (item is AddCurrencyWindow)
                {
                    item.Close();
                }
            }
        }
    }
}
