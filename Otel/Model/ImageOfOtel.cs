using System;

namespace Otel.Model
{
    /// <summary>
    /// Класс, реализующий модель ImageOfOtel
    /// </summary>
    public class ImageOfOtel
    {
        public int ID { get; set; }
        public byte[] Image { get; set; }
        public Nullable<int> OtelID { get; set; }
    }
}