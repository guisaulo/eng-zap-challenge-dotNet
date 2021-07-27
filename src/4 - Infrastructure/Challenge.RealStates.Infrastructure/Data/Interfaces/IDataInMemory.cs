using Challenge.RealStates.Infrastructure.Data;
using System.Collections;

namespace Challenge.RealStates.Infrastructure.Data.Interfaces
{
    public interface IDataInMemory
    {
        public Hashtable Data { get; set; }
        public DataSet Zap { get; set; }
        public DataSet VivaReal { get; set; }
    }
}
