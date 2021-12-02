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
    public class AddCountryViewModel : BaseViewModel
    {
        private AddCountryViewModelController controller;

        private string country;

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public ICommand AddNewCountry { get; private set; }

        public AddCountryViewModel()
        {
            controller = new AddCountryViewModelController();

            AddNewCountry = new DelegateCommand(NewCuntry);
        }

        private async void NewCuntry(object obj)
        {
            if (Country == null)
            {
                MessageBox.Show("Введите новую страну", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            var country = new Country()
            {
                Name = Country
            };

           var newCountry = await controller.CreateCountry(country);

            if (newCountry.ID == 0)
            {
                MessageBox.Show("Такая страна уже есть в базе данных", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

                return;
            }

            MessageBox.Show(newCountry.Name + " добавлена в список", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);

            foreach (Window item in Application.Current.Windows)
            {
                if (item is AddCountryWindow)
                {
                    item.Close();
                }
            }
        }
    }
}
