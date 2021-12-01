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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Otel.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для TheChangeRoleUser.xaml
    /// </summary>
    public partial class TheChangeRoleUser : Page
    {
        private TheChangeRoleUserViewModel changeRoleUserViewModel;

        public TheChangeRoleUser()
        {
            InitializeComponent();

            DataContext = changeRoleUserViewModel = new TheChangeRoleUserViewModel();
        }
    }
}
