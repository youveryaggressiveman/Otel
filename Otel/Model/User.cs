using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel.Model
{
    /// <summary>
    /// Класс, реализующий модель User
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int CountryID { get; set; }
        public int PassportID { get; set; }
        public int RoleID { get; set; }
        public byte[] Avatar { get; set; }

        public ICollection<Card> Card { get; set; }

        public Country Country { get; set; }

        public ICollection<Order> Order { get; set; }

        public Passport Passport { get; set; }

        public Role Role { get; set; }
    }
}
