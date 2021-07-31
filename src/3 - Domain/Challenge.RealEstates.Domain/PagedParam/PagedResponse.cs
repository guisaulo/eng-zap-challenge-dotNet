using System.Collections.Generic;

namespace Challenge.RealEstates.Domain.PagedParam
{
    public class PagedResponse<T>
    {       
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public long TotalCount { get; set; }

        public IEnumerable<T> Listings { get; set; }
    }
}