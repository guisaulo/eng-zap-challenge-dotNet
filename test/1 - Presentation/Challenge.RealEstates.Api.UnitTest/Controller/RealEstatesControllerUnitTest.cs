using Challenge.RealEstates.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challenge.RealEstates.Application.Command;
using Challenge.RealEstates.Application.DTOs;
using Challenge.RealEstates.Application.DTOs.Response;
using Challenge.RealEstates.Application.Interfaces;
using Challenge.RealEstates.Domain.DomainResponse;
using Challenge.RealEstates.Domain.Entities;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Challenge.RealEstates.Gateways.Interfaces;

namespace Challenge.RealEstates.Api.UnitTest.Controller
{
    public class RealEstatesControllerUnitTest
    {
        [Fact]
        public void Should_to_validate_return_ok_when_load_source()
        {
            var controller = new RealEstatesController(InitializeRealEstateApplicationService(), InitializeRealEstateGateway());
            var actionResult = controller.Load(new LoadRealEstatesCommand { Url = "http://grupozap-code-challenge.s3-website-us-east-1.amazonaws.com/sources/source-sample.json" });
            Assert.IsType<ActionResult<LoadRealEstatesCommandResponse>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public void Should_to_validate_return_badrequest_when_load_source()
        {
            var controller = new RealEstatesController(InitializeRealEstateApplicationService(), InitializeRealEstateGateway());
            var actionResult = controller.Load(new LoadRealEstatesCommand { Url = "123" });
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public void Should_to_validate_return_ok_when_getbysource()
        {
            var controller = new RealEstatesController(InitializeRealEstateApplicationService(), InitializeRealEstateGateway());
            var actionResult = controller.GetBySource("zap", new RealEstatesSearchDto());
            Assert.IsType<ActionResult<PagedParamsDto<RealEstateDTO>>>(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        private static IRealEstateApplicationService InitializeRealEstateApplicationService()
        {
            var mock = new Mock<IRealEstateApplicationService>();

            mock.Setup(m => m.AddRange(It.IsAny<IEnumerable<RealEstate>>()))
                .Returns(new LoadRealEstatesCommandResponse());

            mock.Setup(m => m.GetAllPaged(It.IsAny<string>(), It.IsAny<RealEstatesSearchDto>()))
                .Returns((string p, RealEstatesSearchDto f) => new PagedParamsDto<RealEstateDTO>());

            return mock.Object;
        }

        private static IRealEstateGateway InitializeRealEstateGateway()
        {
            var mock = new Mock<IRealEstateGateway>();

            mock.Setup(m => m.GetRealEstatesFromSourceUrl(It.IsAny<string>()))
                .Returns(new List<RealEstate>());

            return mock.Object;
        }
    }
}
