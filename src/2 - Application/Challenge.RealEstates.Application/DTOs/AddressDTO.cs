namespace Challenge.RealEstates.Application.DTOs
{
    public class AddressDTO
    {
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public GeoLocationDTO GeoLocation { get; set; }
    }
}