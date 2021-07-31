using System.Collections.Generic;

namespace Challenge.RealEstates.Domain.DomainResponse
{
    public class RealEstatesResponse
    {
        public ICollection<string> ValidIds { get; set; }
        public ICollection<string> InvalidIds { get; set; }
        public RealEstatesResponse()
        {
            this.ValidIds = new List<string>();
            this.InvalidIds = new List<string>();
        }
    }
}