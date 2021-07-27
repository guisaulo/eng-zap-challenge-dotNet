using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Params;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.RealEstates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZapController : ControllerBase
    {
        private readonly IZapApplicationService _applicationServiceZap;

        public ZapController(IZapApplicationService applicationServiceZap)
        {
            _applicationServiceZap = applicationServiceZap;
        }

        // GET: api/<ZapController>
        [HttpGet("GetAll")]
        public PagedResponseDTO<RealEstateDTO> GetAll([FromQuery] QueryParamsDTO queryParam)
        {
            return _applicationServiceZap.GetAllPaged(queryParam);
        }

        [HttpGet("LoadSource")]
        public bool LoadSource()
        {
            return _applicationServiceZap.LoadSource();
        }
    }
}