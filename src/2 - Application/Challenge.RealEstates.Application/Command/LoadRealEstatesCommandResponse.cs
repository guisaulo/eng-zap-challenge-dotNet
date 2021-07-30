using System.Collections.Generic;

namespace Challenge.RealEstates.Application.DTOs.Response
{
    public class LoadRealEstatesCommandResponse
    {
        public string DateAddRangeCreate { get; set; }

        public long CountInvalidInputIds { get; set; }
        public long CountZapIllegibleIds { get; set; }
        public long CountVivaRealIneligibleIds { get; set; }

        public ICollection<string> InvalidInputIds { get; set; }
        public ICollection<string> ZapIllegibleIds { get; set; }
        public ICollection<string> VivaRealIneligibleIds { get; set; }
    }
}