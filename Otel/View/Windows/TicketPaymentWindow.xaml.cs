using Otel.Model;
using Otel.ViewModel;
using Otel.Windows;
using System.Collections.Generic;
using System.Windows;

namespace Otel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для TicketPaymentWindow.xaml
    /// </summary>
    public partial class TicketPaymentWindow : Window
    {
        private TicketPaymentViewModel ticketViewModel;

        public TicketPaymentWindow(Order order, Hotel hotel)
        {
            DataContext = ticketViewModel = new TicketPaymentViewModel(order, hotel);

            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
