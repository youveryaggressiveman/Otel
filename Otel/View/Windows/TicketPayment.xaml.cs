using Otel.Model;
using Otel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Otel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для TicketPayment.xaml
    /// </summary>
    public partial class TicketPayment : Window
    {
        private TicketPaymentViewModel ticketViewModel;

        public TicketPayment(Ticket ticket, Date date, NameOtel nameOtel, List<Room> rooms, TypeRoom typeRoom, string address)
        {
            DataContext = ticketViewModel = new TicketPaymentViewModel(ticket, date, nameOtel, rooms, typeRoom, address);

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
