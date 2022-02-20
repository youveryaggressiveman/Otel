using System;

namespace Otel.Model
{
    /// <summary>
    /// Класс, реализующий модель Room
    /// </summary>
    public class Room
    {
        public int ID { get; set; }
        public int TypeRoomID { get; set; }
        public int Number { get; set; }
        public int PriceID { get; set; }
        public Nullable<int> OtelID { get; set; }

        public TypeRoom TypeRoom { get; set; }
        public Price Price { get; set; }
    }
}