using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEtates.Domain.Entities;
using System.Collections.Generic;

namespace Challenge.RealEstates.Application.Interfaces
{
    public interface IRealEstateApplicationService
    {
        PagedParamsDto<RealEstateDTO> GetAllPaged(string source, RealEstatesSearchDto queryParam);
        LoadRealEstatesCommandResponse AddRange(IEnumerable<RealEstate> realEstates);
    }
}