using System.Collections;
using System.Collections.Generic;

namespace Challenge.RealStates.Infrastructure.Data.Interfaces
{
    public interface IDataInMemory
    {
        public Hashtable Data { get; set; }
        public List<string> ZapIds { get; set; }
        public List<string> VivaRealIds { get; set; }
        public Filters Filters { get; set; }
    }
}