namespace User.Data.DTO
{
    public class AddressDTO
    {
        public int? Id { get; set; }
        public int? CityId { get; set; }
        public CityDTO City { get; set; }
        public int? CountryId { get; set; }
        public CountryDTO Country { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
    }
}
