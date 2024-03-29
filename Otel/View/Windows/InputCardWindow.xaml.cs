﻿using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Otel.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для InputCardWindow.xaml
    /// </summary>
    public partial class InputCardWindow : Window
    {
        public InputCardWindow()
        {
            InitializeComponent();
        }

        private void cardNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cardNumberTextBox.Text.Length > 0)
            {
                imageNumber.Visibility = Visibility.Visible;

                if (cardNumberTextBox.Text[0] == '2')
                {
                    imageNumber.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Image/payment_mir.png"));

                    return;
                }

                if (cardNumberTextBox.Text[0] == '4')
                {
                    imageNumber.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Image/payment_visa.png"));

                    return;
                }

                if (cardNumberTextBox.Text.Length >= 2)
                {
                    if (cardNumberTextBox.Text[0] == '5' && (cardNumberTextBox.Text[1] == '1' || cardNumberTextBox.Text[1] == '2' ||
                        cardNumberTextBox.Text[1] == '3' || cardNumberTextBox.Text[1] == '4' || cardNumberTextBox.Text[1] == '5'))
                    {
                        imageNumber.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Image/payment_mastercard.png"));

                        return;
                    }
                }
            }

            imageNumber.Visibility = Visibility.Collapsed;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item is TicketPaymentWindow)
                {
                    item.Show();
                }
            }
        }

        private void AllTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
