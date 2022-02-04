using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Otel.Core.Services;

namespace Otel.Core.Utils
{
    public class ImageService : IImageService
    {
        private readonly DirectoryInfo dsInfo;

        public ImageService()
        {
            dsInfo = new DirectoryInfo(@"Resources\Images");
        }

        public Task Save(IEnumerable<BitmapSource> sources, string nameHotel)
        {
            BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();

            int index = 0;

            if (!dsInfo.Exists)
            {
                dsInfo.Create();
            }

            foreach (var item in sources)
            {
                bitmapEncoder = new BmpBitmapEncoder();

                bitmapEncoder.Frames.Add(BitmapFrame.Create(item));

                FileInfo fileInfo = new FileInfo(dsInfo.FullName + @"/" + nameHotel.Replace(" ", "_") + index + ".jpg");

                if (fileInfo.Exists)
                {
                    return Task.CompletedTask;
                }

                using (FileStream fileStream = new FileStream(dsInfo.FullName + @"/" + nameHotel.Replace(" ", "_") + index + ".jpg", FileMode.Create))
                {
                    bitmapEncoder.Save(fileStream);

                    fileStream.Dispose();
                }

                index++;
            }

            return Task.CompletedTask;
        }

        public IEnumerable<Uri> FindImage(string nameHotel)
        {
            var fileList = dsInfo.GetFiles();

            List<Uri> listUri = new List<Uri>();

            foreach (var item in fileList)
            {
                if (item.Name.Contains(nameHotel.Replace(" ", "_")))
                {
                    listUri.Add(new Uri(item.FullName));
                }
            }

            return listUri;
        }
    }
}
