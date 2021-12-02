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
    public class AddCurrencyViewModel : BaseViewModel
    {
        private AddCurrencyViewModelController controller;

        private string currency;

        public string Currency
        {
            get => currency;
            set
            {
                currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public ICommand AddNewCurrency { get; private set; }

        public AddCurrencyViewModel()
        {
            controller = new AddCurrencyViewModelController();

            AddNewCurrency = new DelegateCommand(NewCurrency);
        }

        private async void NewCurrency(object obj)
        {
            if (Currency == null)
            {
                MessageBox.Show("Введите новую валюту", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            var currency = new Currency()
            {
                Name = Currency,
            };

            var newCurrency = await controller.CreateCurrency(currency);

            if (newCurrency.ID == 0)
            {
                MessageBox.Show("Такая валюта уже есть в базе данных", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            MessageBox.Show(newCurrency.Name + " добавлен в список валют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

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
