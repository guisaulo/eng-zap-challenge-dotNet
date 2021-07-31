using System.Collections.Generic;

namespace Challenge.RealEstates.Application.Command
{
    public class RealEstatesCommandResponse
    {
        public long TotalIds { get; set; }
        public ICollection<string> ValidIds { get; set; }
        public ICollection<string> InvalidIds { get; set; }
    }
}
