using System;
using System.Collections.Generic;

namespace Challenge.RealEstates.Application.DTOs.Response
{
    public class PagedParamsDto<TDTO>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public long TotalCount { get; set; }

        public IEnumerable<TDTO> Listings { get; set; }
    }
}