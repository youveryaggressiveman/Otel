using Otel.ViewModel;
using System.Windows.Controls;

namespace Otel.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewTicket.xaml
    /// </summary>
    public partial class NewTicket : Page
    {

        public NewTicket(TicketViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
