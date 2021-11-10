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
    /// Логика взаимодействия для InputCardWindow.xaml
    /// </summary>
    public partial class InputCardWindow : Window
    {
        public InputCardWindow()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[1].Close();
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
    }
}
