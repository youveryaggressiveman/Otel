using System.Text.RegularExpressions;
using Otel.ViewModel;
using Otel.Windows;
using System.Windows;
using System.Windows.Input;

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
            if (ShowPswCheckBox.IsChecked == false)
            {
                pswBox.Password = pswTextBox.Text;
                pswBox.Visibility = Visibility.Visible;
                pswTextBox.Visibility = Visibility.Collapsed;
            }
            else if (ShowPswCheckBox.IsChecked == true)
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

            foreach (Window item in Application.Current.Windows)
            {
                if (item is AuthWindow)
                {
                    item.Close();
                }
            }
        }

        private void PhoneTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string input = e.Text.ToString();
            Regex inputRegex = new Regex(@"^[0-9]*$");
            Match match = inputRegex.Match(input);

            if (!match.Success)
            {
                e.Handled = true;
            }
        }
    }
}
