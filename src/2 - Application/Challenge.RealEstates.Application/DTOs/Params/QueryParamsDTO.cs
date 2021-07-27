﻿namespace Challenge.RealEstates.Application.DTOs.Params
{
    public class QueryParamsDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string UsableAreas { get; set; }
        public string ParkingSpaces { get; set; }
        public string City { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }
        public string BusinessType { get; set; }
        public string Price { get; set; }
    }
}