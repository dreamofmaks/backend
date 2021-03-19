namespace User.Data.DTO
{
    public class AddressDTO
    {
        public CityDTO City { get; set; }
        public CountryDTO Country { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    }
}
