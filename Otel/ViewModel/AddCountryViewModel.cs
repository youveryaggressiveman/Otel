using Otel.Command;
using Otel.Controllers;
using Otel.Model;
using Otel.View.Windows;
using System.Windows;
using System.Windows.Input;

namespace Otel.ViewModel
{
    /// <summary>
    /// Класс, реализующий логику AddCountryWindow
    /// </summary>
    public class AddCountryViewModel : BaseViewModel
    {
        #region fields

        private readonly UniversalController<Country> universalControllerCreateCountry;

        private string country;

        #endregion

        #region properties

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        #endregion

        #region command

        public ICommand AddNewCountry { get; private set; }

        #endregion

        public AddCountryViewModel()
        {
            universalControllerCreateCountry = new UniversalController<Country>();

            AddNewCountry = new DelegateCommand(NewCuntry);
        }

        /// <summary>
        /// Метод, который позволяет интерфейсу добавлять новую страну
        /// </summary>
        /// <param name="obj"></param>
        private async void NewCuntry(object obj)
        {
            if (Country == null)
            {
                HandyControl.Controls.MessageBox.Info("Введите новую страну", "Предупреждение");

                return;
            }

            var country = new Country()
            {
                Name = Country
            };

            var newCountry = await universalControllerCreateCountry.CreateAnother(country, "");

            if (newCountry.ID == 0)
            {
                HandyControl.Controls.MessageBox.Info("Такая страна уже есть в базе данных", "Предупреждение");

                return;
            }

            HandyControl.Controls.MessageBox.Success(newCountry.Name + " добавлена в список", "Предупреждение");

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
