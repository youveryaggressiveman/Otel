using Otel.Controllers;
using Otel.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Otel.ViewModel
{
    public class TicketViewModel : BaseViewModel
    {
        private TicketViewModelController controller;

        private ObservableCollection<CountryOfOtel> countryOfOtelList;
        private ObservableCollection<Hotel> hotelList;
        private ObservableCollection<Room> roomList;
        private ObservableCollection<TypeRoom> typeRoomList;
        private ObservableCollection<NameOtel> nameOtelList;
        private CountryOfOtel selectedCountry;

        public CountryOfOtel SelectedCountry
        {
            get => selectedCountry;
            set
            {
                selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
                LoadOtelByCountry();
            }
        }

        public ObservableCollection<NameOtel> NameOtelList
        {
            get => nameOtelList;
            set
            {
                nameOtelList = value;
                OnPropertyChanged(nameof(NameOtelList));
            }
        }

        public ObservableCollection<TypeRoom> TypeRommList
        {
            get => typeRoomList;
            set
            {
                typeRoomList = value;
                OnPropertyChanged(nameof(TypeRommList));
            }
        }

        public ObservableCollection<Room> RoomList
        {
            get => roomList;
            set
            {
                roomList = value;
                OnPropertyChanged(nameof(RoomList));
            }
        }

        public ObservableCollection<Hotel> HotelList
        {
            get => hotelList;
            set
            {
                hotelList = value;
                OnPropertyChanged(nameof(HotelList));
            }
        }

        public ObservableCollection<CountryOfOtel> CountryOfOtelList
        {
            get => countryOfOtelList;
            set
            {
                countryOfOtelList = value;
                OnPropertyChanged(nameof(CountryOfOtelList));
            }
        }

        public TicketViewModel()
        {
            controller = new TicketViewModelController();
            CountryOfOtelList = new ObservableCollection<CountryOfOtel>();
            HotelList = new ObservableCollection<Hotel>();
            RoomList = new ObservableCollection<Room>();
            TypeRommList = new ObservableCollection<TypeRoom>();
            NameOtelList = new ObservableCollection<NameOtel>();

            LoadAllData();
        }

        private async void LoadOtelByCountry()
        {
            var hotelCountryList = await controller.GetOtelByCountry(SelectedCountry.ID);

            var otelNameList = await controller.GetNameOtlelByOtelList(hotelCountryList);

            NameOtelList = new ObservableCollection<NameOtel>();

            foreach (var item in otelNameList)
            {
                NameOtelList.Add(item);
            }

        }

        private async void LoadAllData()
        {
            var hotel = await controller.GetOtelData();
            var countryOfOtel = await controller.GetCountryData();
            var room = await controller.GetRoomData();
            var typeRoom = await controller.GetTypeRoomData();

            foreach (var item in countryOfOtel)
            {
                CountryOfOtelList.Add(item);
            }

            foreach (var item in hotel)
            {
                HotelList.Add(item);
            }

            foreach (var item in room)
            {
                RoomList.Add(item);
            }

            foreach (var item in typeRoom)
            {
                TypeRommList.Add(item);
            }
        }
    }
}
