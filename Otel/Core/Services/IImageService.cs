using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Otel.Core.Services
{
    /// <summary>
    /// Интерфейс, который помагает управлять изображениями приложения
    /// </summary>
    public interface IImageService
    {
        Task Save(IEnumerable<BitmapSource> sources, string nameHotel);

        IEnumerable<Uri> FindImage(string nameHotel);
    }
}
