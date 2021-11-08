using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel.Model
{
    public class Ticket
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Nullable<int> OtelID { get; set; }
    }
}
