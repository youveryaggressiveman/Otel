﻿using System;
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
    }
}
