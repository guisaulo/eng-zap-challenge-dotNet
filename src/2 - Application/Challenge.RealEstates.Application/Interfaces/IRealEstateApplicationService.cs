using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Domain.Entities;
using System.Collections.Generic;

namespace Challenge.RealEstates.Application.Interfaces
{
    public interface IRealEstateApplicationService
    {
        LoadRealEstatesCommandResponse AddRange(IEnumerable<RealEstate> realEstates);
        PagedParamsDto<RealEstateDTO> GetAllPaged(string source, RealEstatesSearchDto queryParam);
    }
}