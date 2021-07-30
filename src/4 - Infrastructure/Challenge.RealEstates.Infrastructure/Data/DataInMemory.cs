using Challenge.RealEstates.Infrastructure.Data.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace Challenge.RealEstates.Infrastructure.Data
{
    public class DataInMemory : IDataInMemory
    {
        public Hashtable Data { get; set; }
        public HashSet<string> ZapIds { get; set; }
        public HashSet<string> VivaRealIds { get; set; }
        public Dictionary<string, Dictionary<string, HashSet<string>>> Filters { get; set; }

        public DataInMemory()
        {
            this.Data = new Hashtable();
            this.ZapIds = new HashSet<string>();
            this.VivaRealIds = new HashSet<string>();
            this.Filters = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        }
    }
}