using Otel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для AdminMode.xaml
    /// </summary>
    public partial class AdminMode : Page
    {
        private AdminViewModel adminViewModel;

        private ObservableCollection<Uri> listUri;

        public AdminMode()
        {
            InitializeComponent();

            listUri = new ObservableCollection<Uri>();

            DataContext = adminViewModel = new AdminViewModel();
        }

        private void ImageSelector()
        {
            listUri.Add(ImageSelector1.Uri);
            listUri.Add(ImageSelector2.Uri);
            listUri.Add(ImageSelector3.Uri);
            listUri.Add(ImageSelector4.Uri);
            listUri.Add(ImageSelector5.Uri);
            listUri.Add(ImageSelector6.Uri);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ImageSelector();

            (DataContext as AdminViewModel).ListUriImage = listUri;
        }
    }
}
