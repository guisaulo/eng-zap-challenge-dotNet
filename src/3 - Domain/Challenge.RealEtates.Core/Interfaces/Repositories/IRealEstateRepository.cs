using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;

namespace Challenge.RealEtates.Core.Interfaces.Repositories
{
    public interface IRealEstateRepository
    {
        void AddZapRealEstate(RealEstate realEstate);
        void AddVivaRealEstate(RealEstate realEstate);
        PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter);
    }
}