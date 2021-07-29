using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using Challenge.RealStates.Infrastructure.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.RealStates.Infrastructure.Repositories
{
    public class RealEstateRepository : IRealEstateRepository
    {
        private readonly IDataInMemory _dataInMemory;

        public RealEstateRepository(IDataInMemory dataInMemory)
        {
            _dataInMemory = dataInMemory;
        }

        public void AddZapRealEstate(RealEstate realEstate)
        {
            AddRealEstateInData(realEstate);
            CreateFilterBusinessType(realEstate);
            _dataInMemory.ZapIds.Add(realEstate.Id);
        }

        public void AddVivaRealEstate(RealEstate realEstate)
        {
            AddRealEstateInData(realEstate);
            CreateFilterBusinessType(realEstate);
            _dataInMemory.VivaRealIds.Add(realEstate.Id);
        }

        private void CreateFilterBusinessType(RealEstate realEstate)
        {
            if (_dataInMemory.Filters.UsableAreas.ContainsKey(realEstate.UsableAreas))
                _dataInMemory.Filters.UsableAreas[realEstate.UsableAreas].Add(realEstate.Id);
            else
                _dataInMemory.Filters.UsableAreas.Add(realEstate.UsableAreas, new HashSet<string> { realEstate.Id });

            if (_dataInMemory.Filters.ParkingSpaces.ContainsKey(realEstate.ParkingSpaces))
                _dataInMemory.Filters.ParkingSpaces[realEstate.ParkingSpaces].Add(realEstate.Id);
            else
                _dataInMemory.Filters.ParkingSpaces.Add(realEstate.ParkingSpaces, new HashSet<string> { realEstate.Id });

            if (_dataInMemory.Filters.City.ContainsKey(realEstate.Address.City))
                _dataInMemory.Filters.City[realEstate.Address.City].Add(realEstate.Id);
            else
                _dataInMemory.Filters.City.Add(realEstate.Address.City, new HashSet<string> { realEstate.Id });

            if (_dataInMemory.Filters.Bathrooms.ContainsKey(realEstate.Bathrooms))
                _dataInMemory.Filters.Bathrooms[realEstate.Bathrooms].Add(realEstate.Id);
            else
                _dataInMemory.Filters.Bathrooms.Add(realEstate.Bathrooms, new HashSet<string> { realEstate.Id });

            if (_dataInMemory.Filters.Bedrooms.ContainsKey(realEstate.Bedrooms))
                _dataInMemory.Filters.Bedrooms[realEstate.Bedrooms].Add(realEstate.Id);
            else
                _dataInMemory.Filters.Bedrooms.Add(realEstate.Bedrooms, new HashSet<string> { realEstate.Id });

            if (_dataInMemory.Filters.BusinessType.ContainsKey(realEstate.PricingInfos.BusinessType))
                _dataInMemory.Filters.BusinessType[realEstate.PricingInfos.BusinessType].Add(realEstate.Id);
            else
                _dataInMemory.Filters.BusinessType.Add(realEstate.PricingInfos.BusinessType, new HashSet<string> { realEstate.Id });

            if (_dataInMemory.Filters.Price.ContainsKey(realEstate.PricingInfos.Price))
                _dataInMemory.Filters.Price[realEstate.PricingInfos.Price].Add(realEstate.Id);
            else
                _dataInMemory.Filters.Price.Add(realEstate.PricingInfos.Price, new HashSet<string> { realEstate.Id });
        }

        private void AddRealEstateInData(RealEstate realEstate)
        {
            if(!_dataInMemory.Data.ContainsKey(realEstate.Id))
                _dataInMemory.Data.Add(realEstate.Id, realEstate);
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter)
        {
            return new PagedResponse<RealEstate>
            {
                PageNumber = pagedParams.PageNumber,
                PageSize = pagedParams.PageSize,
                TotalCount = GetListRealEstateFake().ToList().Count(),
                Listings = GetListRealEstateFake()
            };
        }

        private static List<RealEstate> GetListRealEstateFake() =>
            new()
            {

                GetRealEstateFake(id: "1", businessType: "RENT"),
                GetRealEstateFake(id: "2", businessType: "RENT"),
                GetRealEstateFake(id: "3", businessType: "SALE"),
                GetRealEstateFake(id: "4", businessType: "SALE"),
                GetRealEstateFake(id: "5", businessType: "SALE"),
            };

        private static RealEstate GetRealEstateFake(string id = "", string businessType = "")
        {
            return new RealEstate
            {
                UsableAreas = 69,
                ListingType = "USED",
                CreatedAt = "2016-11-16T04:14:02Z",
                ListingStatus = "ACTIVE",
                Id = id,
                ParkingSpaces = 1,
                UpdatedAt = "2016-11-16T04:14:02Z",
                Owner = false,
                Images = new List<string> {
                     "https://resizedimgs.vivareal.com/crop/400x300/vr.images.sp/285805119ab0761500127aebd8ab0e1d.jpg",
                     "https://resizedimgs.vivareal.com/crop/400x300/vr.images.sp/4af1656b66b9e12efff6ce06f51926f6.jpg",
                     "https://resizedimgs.vivareal.com/crop/400x300/vr.images.sp/895f0d4ce1e641fd5c3aad48eff83ac8.jpg",
                     "https://resizedimgs.vivareal.com/crop/400x300/vr.images.sp/e7b5cce2d9aee78867328dfa0a7ba4c6.jpg",
                     "https://resizedimgs.vivareal.com/crop/400x300/vr.images.sp/d833da4cdf6b25b7acf3ae0710d3286d.jpg"
                },
                Address = new Address
                {
                    City = "",
                    Neighborhood = "",
                    GeoLocation = new GeoLocation
                    {
                        Location = new Location
                        {
                            Lat = -46.716542,
                            Lon = -23.502555
                        }
                    }
                },
                Bathrooms = 2,
                Bedrooms = 3,
                PricingInfos = new PricingInfos
                {
                    YearlyIptu = "0",
                    Price = "405000",
                    BusinessType = businessType,
                    MonthlyCondoFee = "495",
                }
            };
        }


    }
}