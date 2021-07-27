using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Params;
using Challenge.RealEstates.Application.DTOs.Response;

namespace Challenge.RealEstates.Application.Interfaces
{
    public interface IZapApplicationService
    {
        bool LoadSource();
        PagedResponseDTO<RealEstateDTO> GetAllPaged(QueryParamsDTO queryParam);
    }
}