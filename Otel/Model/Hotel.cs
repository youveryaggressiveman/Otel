using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel.Model
{
    /// <summary>
    /// Класс, реализующий модель Hotel
    /// </summary>
    public class Hotel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AddressOfOtelID { get; set; }
        public int DiscriptionID { get; set; }

        public AddressOfOtel AddressOfOtel { get; set; }
        public Discription Discription { get; set; }
        public ICollection<ImageOfOtel> ImageOfOtel { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Room> Room { get; set; }
    }
}
