using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;

namespace Challenge.RealEtates.Core.Interfaces.Services
{
    public interface IZapService
    {
        bool LoadSource();
        PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter);
    }
}
