using Challenge.RealEstates.Domain.Entities;
using System.Collections.Generic;

namespace Challenge.RealEstates.Domain.PagedParam
{
    public class PagedResponse<TDTO>
    {       
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public long TotalCount { get; set; }

        public IEnumerable<TDTO> Listings { get; set; }
    }
}