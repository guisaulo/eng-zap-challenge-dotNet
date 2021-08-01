using Challenge.RealEstates.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.RealEstates.Domain.Services.UnitTest.RealEstate.Faker
{
    public static class RealEstateFaker
    {
        public static IQueryable<Entities.RealEstate> GetRealEstatesZapAsQueryable() =>
            new List<Entities.RealEstate>
            {
                GetRealEstate("58815", "Belo Horizonte", "SALE", 2, 3, 1),
                GetRealEstate("58817", "Belo Horizonte", "SALE", 3, 3, 1),
                GetRealEstate("58818", "Belo Horizonte", "RENTAL", 3, 3, 1),
                GetRealEstate("58818", "Contagem", "SALE", 2, 3, 1),
                GetRealEstate("58818", "Contagem", "RENTAL", 2, 2, 0),
            }.AsQueryable();


        public static IQueryable<Entities.RealEstate> GetRealEstatesVivaRealAsQueryable() =>
            new List<Entities.RealEstate>
            {
                GetRealEstate("68815", "Belo Horizonte", "SALE", 2, 3, 1),
                GetRealEstate("68817", "Belo Horizonte", "SALE", 3, 3, 1),
                GetRealEstate("68818", "Belo Horizonte", "RENTAL", 3, 3, 1),
                GetRealEstate("68818", "Contagem", "SALE", 2, 3, 1),
                GetRealEstate("68818", "Contagem", "RENTAL", 2, 2, 0),
            }.AsQueryable();

        private static Entities.RealEstate GetRealEstate(string id, string city, string businessType, int bathrooms, int bedrooms, int parkingSpaces) =>
            new()
            {
                Id = id,
                Address = new Address()
                {
                    City = city
                },
                PricingInfos = new PricingInfos()
                {
                    BusinessType = businessType
                },
                Bathrooms = bathrooms,
                Bedrooms = bedrooms,
                ParkingSpaces = parkingSpaces
            };


    }
}