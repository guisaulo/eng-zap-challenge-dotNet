using Challenge.RealEtates.Core.Interfaces.Repositories;
using Challenge.RealEtates.Core.Interfaces.Services;
using Challenge.RealEtates.Domain.Entities;
using Challenge.RealEtates.Domain.Filter;
using Challenge.RealEtates.Domain.PagedParam;
using System.Collections.Generic;

namespace Challenge.RealEtates.Services
{
    public class ZapService : IZapService
    {
        private readonly IZapRepository _zapRepository;
        public ZapService(IZapRepository zapRepository)
        {
            _zapRepository = zapRepository;
        }

        public PagedResponse<RealEstate> GetAllPaged(PagedParams pagedParams, Filter filter)
        {
            return _zapRepository.GetAllPaged(pagedParams, filter);
        }

        public bool LoadSource()
        {
            return _zapRepository.LoadSource();
        }
    }
}