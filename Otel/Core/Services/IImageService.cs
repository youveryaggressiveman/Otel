using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Otel.Core.Services
{
    public interface IImageService
    {
        Task Save(IEnumerable<BitmapSource> sources, string nameHotel);

        IEnumerable<Uri> FindImage(string nameHotel);
    }
}
