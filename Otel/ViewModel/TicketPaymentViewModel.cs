using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otel.ViewModel
{
    public class TicketPaymentViewModel : BaseViewModel
    {
        private string country;
        private string nameHotel;
        private string typeRoom;
        private int numberOfHotel;
        private System.DateTime arrivalDate;
        private System.DateTime departureDate;
        private int price;
        private string valueOfPrice;

        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));
            }
        }

        public string NameHotel
        {
            get => nameHotel;
            set
            {
                nameHotel = value;
                OnPropertyChanged(nameof(NameHotel));
            }
        }

        public string TypeRoom
        {
            get => typeRoom;
            set
            {
                typeRoom = value;
                OnPropertyChanged(nameof(TypeRoom));
            }
        }

        public int NumberOfHotel
        {
            get => numberOfHotel;
            set
            {
                numberOfHotel = value;
                OnPropertyChanged(nameof(NumberOfHotel));
            }
        }

        public System.DateTime ArrivalDate
        {
            get => arrivalDate;
            set
            {
                arrivalDate = value;
                OnPropertyChanged(nameof(ArrivalDate));
            }
        }

        public System.DateTime DepartureDate
        {
            get => departureDate;
            set
            {
                departureDate = value;
                OnPropertyChanged(nameof(DepartureDate));
            }
        }

        public int Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string ValueOfPrice
        {
            get => valueOfPrice;
            set
            {
                valueOfPrice = value;
                OnPropertyChanged(nameof(ValueOfPrice));
            }
        }

        public TicketPaymentViewModel()
        {

        }
    }
}
