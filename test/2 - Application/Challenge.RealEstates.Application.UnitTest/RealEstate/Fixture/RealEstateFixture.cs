using AutoMapper;
using Challenge.RealEstates.Domain.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Challenge.RealEstates.Application.Mappers;
using Challenge.RealEstates.Domain.DomainResponse;
using Challenge.RealEstates.Domain.Filter;
using Challenge.RealEstates.Domain.PagedParam;
using Moq;

namespace Challenge.RealEstates.Application.UnitTest.RealEstate.Fixture
{
    public class RealEstateFixture
    {
        public readonly RealEstateApplicationService Service;

        public RealEstateFixture()
        {
            Service = new RealEstateApplicationService(InitializeRealEstateService(), InitializeRealEstateAutoMapper());
        }

        private static IRealEstateService InitializeRealEstateService()
        {
            var mock = new Mock<IRealEstateService>();

            mock.Setup(m => m.AddRange(It.IsAny<IEnumerable<Domain.Entities.RealEstate>>()))
                .Returns(new AddRangeResponse());
            return mock.Object;

            mock.Setup(m => m.GetAllPaged(It.IsAny<PagedParams>(), It.IsAny<Filters>()))
                .Returns((PagedParams p, Filters f) => GetRealEstates(p, f));
            return mock.Object;

            throw new NotImplementedException();
        }

        private static PagedResponse<Domain.Entities.RealEstate> GetRealEstates(PagedParams pagedParams, Filters filters)
        {
            var realEstate = new Domain.Entities.RealEstate();
            return new PagedResponse<Domain.Entities.RealEstate>()
            {
                PageNumber = pagedParams.PageNumber,
                PageSize = pagedParams.PageSize,
                TotalCount = 1,
                Listings = new List<Domain.Entities.RealEstate>() { realEstate }
            };
        }

        private static IMapper InitializeRealEstateAutoMapper()
        {
            var config = new MapperConfiguration(opts => { opts.AddMaps(Assembly.GetAssembly(typeof(RealEstateProfile))); });
            return config.CreateMapper();
        }
    }
}
