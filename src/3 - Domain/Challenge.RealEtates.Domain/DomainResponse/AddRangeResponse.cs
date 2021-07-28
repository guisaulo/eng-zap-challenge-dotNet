using System.Collections.Generic;

namespace Challenge.RealEtates.Domain.DomainResponse
{
    public class AddRangeResponse
    {
        public ICollection<string> InvalidInputIds { get; set; }
        public ICollection<string> ZapIllegibleIds { get; set; }
        public ICollection<string> VivaRealIneligibleIds { get; set; }
    }
}
