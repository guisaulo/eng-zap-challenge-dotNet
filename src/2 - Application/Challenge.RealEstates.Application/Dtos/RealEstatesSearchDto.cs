namespace Challenge.RealEstates.Application.DTOs
{
    public class RealEstatesSearchDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string City { get; set; }
        public string BusinessType { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }
        public string ParkingSpaces { get; set; }
    }
}