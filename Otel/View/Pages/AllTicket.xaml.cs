using Otel.ViewModel;
using System.Windows.Controls;

namespace Otel.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllTicket.xaml
    /// </summary>
    public partial class AllTicket : Page
    {
        private AllTicketViewModel allTicketViewModel;

        public AllTicket()
        {
            InitializeComponent();

            DataContext = allTicketViewModel = new AllTicketViewModel();
        }
    }
}
