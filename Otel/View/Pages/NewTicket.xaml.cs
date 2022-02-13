using System;
using System.Windows;
using Otel.ViewModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Otel.Core.Services;
using Otel.Core.Utils;

namespace Otel.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewTicket.xaml
    /// </summary>
    public partial class NewTicket : Page
    {

        public NewTicket(TicketViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void NameOtelBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IImageService imageService = new ImageService();

            if ((DataContext as TicketViewModel).SelectedHotel == null)
            {
                Carousel.ItemsSource = new BitmapImage[]
                {
                   new BitmapImage( new Uri(@"pack://application:,,,/Otel;component/Resources/Image/SD-default-image.png"))
                }; 

                return;
            }

            Carousel.ItemsSource = imageService.FindImage((DataContext as TicketViewModel)?.SelectedHotel?.Name);
        }
    }
}
