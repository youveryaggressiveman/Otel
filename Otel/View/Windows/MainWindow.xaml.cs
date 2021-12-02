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

            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            FrameManager.MainFrame = Main;
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));
        }

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("Непредвиденная ошибка: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
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

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));
        }

        private void AdminButtom_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new AdminMode());
        }

        private void AllTicketButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new AllTicket());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));
        }

        private void TheChangeRole_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new TheChangeRoleUser());
        }
    }
}
