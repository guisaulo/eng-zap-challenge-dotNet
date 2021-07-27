using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System.Collections.Generic;

namespace Challenge.RealEtates.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IRealEstateRepository _zapRepository;
        public RealEstateService(IRealEstateRepository zapRepository)
        {
            _zapRepository = zapRepository;
        }

        public void AddRange(IEnumerable<RealEstate> realEstates)
        {
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter)
        {
            return _zapRepository.GetAllPaged(pagedParams, filter);
        }
    }
}