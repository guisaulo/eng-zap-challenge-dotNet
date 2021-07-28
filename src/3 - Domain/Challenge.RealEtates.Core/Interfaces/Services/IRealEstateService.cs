using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System.Collections.Generic;
using Challenge.RealEtates.Domain.DomainResponse;

namespace Challenge.RealEtates.Core.Interfaces.Services
{
    public interface IRealEstateService {
        AddRangeResponse AddRange(IEnumerable<RealEstate> realEstates);

        PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter);
    }
}
