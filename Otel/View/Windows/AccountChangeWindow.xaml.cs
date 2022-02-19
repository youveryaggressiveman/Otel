using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Otel.ViewModel;

namespace Otel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AccountChangeWindow.xaml
    /// </summary>
    public partial class AccountChangeWindow : Window
    {
        public AccountChangeWindow()
        {
            InitializeComponent();

            
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
