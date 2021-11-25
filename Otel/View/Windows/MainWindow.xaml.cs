using Otel.Core;
using Otel.View.Pages;
using Otel.View.Windows;
using Otel.ViewModel;
using System.Windows;

namespace Otel.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FrameManager.MainFrame = Main;
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));
        }

        private void ComeButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }

        private void RegistrButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Application.Current.Windows[0].Close();
        }

        private void allTicketButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new AllTicket());
            allTicketButton.Visibility = Visibility.Collapsed;
            homeButton.Visibility = Visibility.Visible;
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));
            allTicketButton.Visibility = Visibility.Visible;
            homeButton.Visibility = Visibility.Collapsed;
        }
    }
}
