using Otel.View.Pages;
using Otel.View.Windows;
using System.Windows;

namespace Otel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Navigate(new NewTicket());
        }

        private void ComeButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
