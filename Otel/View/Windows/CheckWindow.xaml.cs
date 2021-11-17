using Otel.Model;
using Otel.ViewModel;
using System.Windows;

namespace Otel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для CheckWindow.xaml
    /// </summary>
    public partial class CheckWindow : Window
    {
        public CheckWindow(Order order, Hotel hotel)
        {
            InitializeComponent();

            CheckViewModel checkViewModel = new CheckViewModel(order, hotel);

            DataContext = checkViewModel;
        }
    }
}
