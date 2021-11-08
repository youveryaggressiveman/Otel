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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void ShowPswCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(ShowPswCheckBox.IsChecked== false)
            {
                pswBox.Password = pswTextBox.Text;
                pswBox.Visibility = Visibility.Visible;
                pswTextBox.Visibility = Visibility.Collapsed;
            }
            else if(ShowPswCheckBox.IsChecked == true)
            {
                pswTextBox.Text = pswBox.Password;
                pswTextBox.Visibility = Visibility.Visible;
                pswBox.Visibility = Visibility.Collapsed;
            }
        }

        private void pswBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            (DataContext as AuthViewModel).Password = pswBox.Password;
        }

        private void registrLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            Application.Current.Windows[0].Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
