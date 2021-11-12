using System.Collections.Generic;

namespace Otel.Model
{
    public class Order
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public int OtelID { get; set; }
        public System.DateTime ArrivalDate { get; set; }
        public System.DateTime DepartureDate { get; set; }

        public ICollection<Room> Room { get; set; }
    }
}