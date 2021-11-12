using System;

namespace Otel.Model
{
    public class ImageOfOtel
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> OtelID { get; set; }
    }
}