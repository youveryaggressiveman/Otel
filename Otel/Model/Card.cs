using System;

namespace Otel.Model
{
    public class Card
    {
        public int ID { get; set; }
        public int HashCode { get; set; }
        public int ClientID { get; set; }
        public Nullable<int> LastFourDigits { get; set; }
    }
}