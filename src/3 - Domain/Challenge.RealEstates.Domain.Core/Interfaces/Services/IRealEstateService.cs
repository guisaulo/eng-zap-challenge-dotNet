using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using System.Collections.Generic;
using Challenge.RealEstates.Domain.DomainResponse;

namespace Challenge.RealEstates.Domain.Core.Interfaces.Services
{
    public interface IRealEstateService {
        AddRangeResponse AddRange(IEnumerable<RealEstate> realEstates);

        PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filters filter);
    }
}
