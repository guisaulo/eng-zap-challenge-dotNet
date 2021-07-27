using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using Challenge.RealStates.Infrastructure.Data.Interfaces;
using System.Collections.Generic;

namespace Challenge.RealStates.Infrastructure.Repositories
{
    public class RealEstateRepository : IRealEstateRepository
    {
        private readonly IDataInMemory _dataInMemory;

        public RealEstateRepository(IDataInMemory dataInMemory)
        {
            _dataInMemory = dataInMemory;
        }

        public void Add(RealEstate realEstate)
        {
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter)
        {
            //var teste = _dataInMemory;

            //var dataIdsFilter = new List<string>();
            //if (filter.BusinessType != null)
            //{
            //    dataIdsFilter = _dataInMemory.Value.Zap.Filters.BusinessType.Values.OfType<string>().Where(k => k.Equals(filter.BusinessType)).ToList();
            //}

            //var dataList = new List<RealEstate>();

            //foreach (DictionaryEntry data in _dataInMemory.Value.Zap.Data)
            //{
            //    if (dataIdsFilter.Contains(data.Key))
            //        dataList.Add((RealEstate)data.Value);
            //}

            //var totalCount = dataList.Count();
            //dataList = dataList.Skip(pagedParams.PageNumber * pagedParams.PageSize).Take(pagedParams.PageSize).ToList();

            //return new PagedResponse<RealEstate>
            //{
            //    PageNumber = pagedParams.PageNumber,
            //    PageSize = pagedParams.PageSize,
            //    TotalCount = dataList.Count(),
            //    Listings = dataList
            //};
            return null;
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

        //public bool LoadSource()
        //{

        //    //_dataInMemory.Number = 1;

        //    //_dataInMemory.Value.InstantiateDataInMemory();

        //    //_dataInMemory.Value.Zap.Data.Add("1", GetRealEstateFake(id: "1", businessType: "RENT"));
        //    //_dataInMemory.Value.Zap.Data.Add("2", GetRealEstateFake(id: "2", businessType: "RENT"));
        //    //_dataInMemory.Value.Zap.Data.Add("3", GetRealEstateFake(id: "3", businessType: "RENT"));
        //    //_dataInMemory.Value.Zap.Data.Add("4", GetRealEstateFake(id: "4", businessType: "RENT"));
        //    //_dataInMemory.Value.Zap.Data.Add("5", GetRealEstateFake(id: "5", businessType: "SALE"));
        //    //_dataInMemory.Value.Zap.Data.Add("6", GetRealEstateFake(id: "6", businessType: "SALE"));
        //    //_dataInMemory.Value.Zap.Data.Add("7", GetRealEstateFake(id: "7", businessType: "SALE"));
        //    //_dataInMemory.Value.Zap.Data.Add("8", GetRealEstateFake(id: "8", businessType: "SALE"));
        //    //_dataInMemory.Value.Zap.Data.Add("9", GetRealEstateFake(id: "9", businessType: "SALE"));
        //    //_dataInMemory.Value.Zap.Data.Add("10", GetRealEstateFake(id: "10", businessType: "SALE"));

        //    //_dataInMemory.Value.Zap.Filters.BusinessType = new Hashtable
        //    //{
        //    //    { "RENT", new List<string> { "1", "2", "3", "4" } },
        //    //    { "SALE", new List<string> { "5", "6", "7", "8", "9", "10" } }
        //    //};

        //    return true;
        //}
    }
}