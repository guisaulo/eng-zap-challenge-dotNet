using Challenge.RealEstates.Domain.Core.Interfaces.Repositories;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using Challenge.RealEstates.Infrastructure.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.RealEstates.Infrastructure.Repositories
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
            AddFilters(realEstate);
            _dataInMemory.ZapIds.Add(realEstate.Id);
        }

        public void AddVivaRealEstate(RealEstate realEstate)
        {
            AddRealEstateInData(realEstate);
            AddFilters(realEstate);
            _dataInMemory.VivaRealIds.Add(realEstate.Id);
        }

        private void AddRealEstateInData(RealEstate realEstate)
        {
            if (!_dataInMemory.Data.ContainsKey(realEstate.Id))
                _dataInMemory.Data.Add(realEstate.Id, realEstate);
        }

        private void AddFilters(RealEstate realEstate)
        {
            AddFilter(nameof(realEstate.Address.City), realEstate.Address.City, realEstate.Id);
            AddFilter(nameof(realEstate.PricingInfos.BusinessType), realEstate.PricingInfos.BusinessType, realEstate.Id);
            AddFilter(nameof(realEstate.Bathrooms), realEstate.Bathrooms.ToString(), realEstate.Id);
            AddFilter(nameof(realEstate.Bedrooms), realEstate.Bedrooms.ToString(), realEstate.Id);
            AddFilter(nameof(realEstate.ParkingSpaces), realEstate.ParkingSpaces.ToString(), realEstate.Id);
        }

        public void AddFilter(string filterName, string filterValue, string realEstateId)
        {
            if (string.IsNullOrEmpty(filterValue))
                return;

            if (_dataInMemory.Filters.ContainsKey(filterName))
            {
                var filter = _dataInMemory.Filters[filterName];
                if (filter.ContainsKey(filterValue))
                    filter[filterValue].Add(realEstateId);
                else
                    filter.Add(filterValue, new HashSet<string> { realEstateId });
            }
            else
            {
                _dataInMemory.Filters.Add(filterName, new Dictionary<string, HashSet<string>>());
                _dataInMemory.Filters[filterName].Add(filterValue, new HashSet<string>() { realEstateId });
            }
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filters filters)
        {
            var ids = filters.Source == "zap" ? _dataInMemory.ZapIds.ToHashSet() : _dataInMemory.VivaRealIds.ToHashSet();

            if (!string.IsNullOrEmpty(filters.City))
                ids.IntersectWith(GetFilter(nameof(filters.City), filters.City));
            if (!string.IsNullOrEmpty(filters.BusinessType))
                ids.IntersectWith(GetFilter(nameof(filters.BusinessType), filters.BusinessType));
            if (!string.IsNullOrEmpty(filters.Bathrooms))
                ids.IntersectWith(GetFilter(nameof(filters.Bathrooms), filters.Bathrooms));
            if (!string.IsNullOrEmpty(filters.Bedrooms))
                ids.IntersectWith(GetFilter(nameof(filters.Bedrooms), filters.Bedrooms));
            if (!string.IsNullOrEmpty(filters.ParkingSpaces))
                ids.IntersectWith(GetFilter(nameof(filters.ParkingSpaces), filters.ParkingSpaces));

            return GetPagedResponse(pagedParams, ids);
        }

        public HashSet<string> GetFilter(string filterName, string filterValue)
        {
            var filter = _dataInMemory.Filters[filterName];
            
            var ids = new HashSet<string>();

            if (filter.ContainsKey(filterValue))
                ids = filter[filterValue];
            
            return ids;
        }

        private PagedResponse<RealEstate> GetPagedResponse(PagedParams pagedParams, HashSet<string> ids)
        {
            var listIds = ids.ToList();
            var pageSize = listIds.Count < pagedParams.PageSize ? listIds.Count : pagedParams.PageSize;
            var pagedIds = listIds.Skip((pagedParams.PageNumber - 1) * pageSize).Take(pageSize).ToList();
            var listRealEstate = pagedIds.Select(id => (RealEstate)_dataInMemory.Data[id]).ToList();

            return new PagedResponse<RealEstate>()
            {
                PageNumber = pagedParams.PageNumber,
                PageSize = pagedParams.PageSize,
                TotalCount = listIds.Count,
                Listings = listRealEstate
            };
        }
    }
}