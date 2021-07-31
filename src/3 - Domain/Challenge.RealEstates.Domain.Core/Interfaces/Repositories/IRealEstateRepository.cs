using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;

namespace Challenge.RealEstates.Domain.Core.Interfaces.Repositories
{
    public interface IRealEstateRepository
    {
        void AddZapRealEstate(RealEstate realEstate);
        void AddVivaRealEstate(RealEstate realEstate);
        PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filters filters);
    }
}