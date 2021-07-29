using Challenge.RealStates.Infrastructure.Data.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Challenge.RealStates.Infrastructure.Data
{
    public class DataInMemory : IDataInMemory
    {
        public Hashtable Data { get; set; }
        public List<string> ZapIds { get; set; }
        public List<string> VivaRealIds { get; set; }
        public Filters Filters { get; set; }
        public DataInMemory()
        {
            this.Data = new Hashtable();
            this.ZapIds = new List<string>();
            this.VivaRealIds = new List<string>();
            this.Filters = new Filters();
        }
    }
}