namespace Otel.Model
{
    public class AddressOfOtel
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int CountryOfOtelID { get; set; }

        public Country Country { get; set; }
    }
}