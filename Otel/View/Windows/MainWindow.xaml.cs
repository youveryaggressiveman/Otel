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

            HandyControl.Controls.MessageBox.Error(errorMessage, "Ошибка");
            e.Handled = true;
        }

        private void HomeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));
        }

        private void AdminRadioButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new AdminMode());
        }

        private void AllTicketRadioButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new AllTicket());
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new NewTicket(DataContext as TicketViewModel));

            homeRadioButton.IsChecked = true;
        }

        private void TheChangeRoleRadioButton_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.SetSource(new TheChangeRoleUser());
        }
    }
}
