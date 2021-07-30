using System.Collections.Generic;

namespace Challenge.RealEstates.Application.DTOs
{
    public class RealEstateDTO
    {
        public int UsableAreas { get; set; }
        public string ListingType { get; set; }
        public string CreatedAt { get; set; }
        public string ListingStatus { get; set; }
        public string Id { get; set; }
        public int ParkingSpaces { get; set; }
        public string UpdatedAt { get; set; }
        public bool Owner { get; set; }
        public IEnumerable<string> Images { get; set; }
        public AddressDTO Address { get; set; }
        public int Bathrooms { get; set; }
        public int Bedrooms { get; set; }
        public PricingInfosDTO PricingInfos { get; set; }
    }
}
