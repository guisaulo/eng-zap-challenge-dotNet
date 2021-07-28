using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Params;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEstates.Gateways.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.RealEstates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstatesController : ControllerBase
    {
        private readonly IRealEstateApplicationService _realEstateApplicationService;
        private readonly IRealEstateGateway _realStateGateway;

        public RealEstatesController(IRealEstateApplicationService realEstateApplicationService, IRealEstateGateway realEstateGateway)
        {
            _realEstateApplicationService = realEstateApplicationService;
            _realStateGateway = realEstateGateway;
        }

        [HttpPost("AddFromSourceURL")]
        public AddRangeResponseDto AddFromSourceURL(string sourceURL)
        {
            var realEstates = _realStateGateway.GetRealEstatesFromSourceURL(sourceURL);
            var result = _realEstateApplicationService.AddRange(realEstates);
            return result;
        }

        // GET: api/<ZapController>

        [HttpGet("Zap")]
        public PagedResponseDTO<RealEstateDTO> GetAllZap([FromQuery] QueryParamsDTO queryParam)
        {
            queryParam.Source = "ZAP";
            return _realEstateApplicationService.GetAllPaged(queryParam);
        }

        [HttpGet("VivaReal")]
        public PagedResponseDTO<RealEstateDTO> GetAllVivaReal([FromQuery] QueryParamsDTO queryParam)
        {
            queryParam.Source = "VIVAREAL";
            return _realEstateApplicationService.GetAllPaged(queryParam);
        }

    }
}