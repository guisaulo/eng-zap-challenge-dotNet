using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Params;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEtates.Domain.Entities;
using System.Collections.Generic;

namespace Challenge.RealEstates.Application.Interfaces
{
    public interface IRealEstateApplicationService
    {
        PagedResponseDTO<RealEstateDTO> GetAllPaged(QueryParamsDTO queryParam);
        AddRangeResponseDto AddRange(IEnumerable<RealEstate> realEstates);
    }
}