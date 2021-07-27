using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System.Collections.Generic;

namespace Challenge.RealEtates.Core.Interfaces.Services
{
    public interface IRealEstateService { 
        PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter);
        void AddRange(IEnumerable<RealEstate> realEstates);
    }
}
