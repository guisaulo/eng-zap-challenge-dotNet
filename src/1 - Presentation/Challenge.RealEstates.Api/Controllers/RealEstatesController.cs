using Challenge.RealEstates.Api.Properties;
using Challenge.RealEstates.Application.Command;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEstates.Gateways.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Challenge.RealEstates.Api.Controllers
{
    [Route("realestates")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class RealEstatesController : ControllerBase
    {
        private readonly IRealEstateApplicationService _realEstateApplicationService;
        private readonly IRealEstateGateway _realStateGateway;

        public RealEstatesController(IRealEstateApplicationService realEstateApplicationService, IRealEstateGateway realEstateGateway)
        {
            _realEstateApplicationService = realEstateApplicationService;
            _realStateGateway = realEstateGateway;
        }

        /// <summary>
        /// Load new real estates into memory from a input url.
        /// </summary>
        /// <param name="command"><see cref="LoadRealEstatesCommand"/></param>
        /// <returns>Load Informations<see cref="LoadRealEstatesCommandResponse"/></returns>
        [HttpPost("load")]
        public ActionResult<LoadRealEstatesCommandResponse> Load([FromBody] LoadRealEstatesCommand command)
        {
            if (!IsValidCommand(command))
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]> { { "error", new[] { Resource.InvalidUrl } } }));

            var realEstatesFromUrl = _realStateGateway.GetRealEstatesFromSourceUrl(command.Url);
            var result = _realEstateApplicationService.AddRange(realEstatesFromUrl);
            return Ok(result);
        }

        /// <summary>
        /// Get all real estates by source.
        /// </summary>
        /// <returns>List of <see cref="RealEstateDTO"/></returns>

        [HttpGet("{source}")]
        public ActionResult<PagedParamsDto<RealEstateDTO>> GetBySource(string source, [FromQuery] RealEstatesSearchDto search)
        {
            var result = _realEstateApplicationService.GetAllPaged(source, search);
            return Ok(result);
        }

        private static bool IsValidCommand(LoadRealEstatesCommand source)
        {
            return source != null && IsValidUrl(source.Url);
        }

        private static bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            try
            {
                var uriResult = new Uri(url);
                return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
            }
            catch
            {
                return false;
            }
        }
    }
}