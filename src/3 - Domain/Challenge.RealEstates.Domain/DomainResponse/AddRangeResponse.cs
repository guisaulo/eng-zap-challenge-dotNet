﻿using System.Collections.Generic;

namespace Challenge.RealEstates.Domain.DomainResponse
{
    public class AddRangeResponse
    {
        public ICollection<string> InvalidInputIds { get; set; }
        public ICollection<string> ZapIllegibleIds { get; set; }
        public ICollection<string> VivaRealIneligibleIds { get; set; }
    }
}