using System.Collections;
using System.Collections.Generic;

namespace Challenge.RealEstates.Infrastructure.Data.Interfaces
{
    public interface IDataInMemory
    {
        public Hashtable Data { get; set; }
        public HashSet<string> ZapIds { get; set; }
        public HashSet<string> VivaRealIds { get; set; }
        public Dictionary<string, Dictionary<string, HashSet<string>>> Filters { get; set; }
    }
}