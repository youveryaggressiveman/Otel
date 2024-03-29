﻿using Otel.Windows;
using System.Text.RegularExpressions;
using System.Windows;

namespace Otel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void phoneTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
