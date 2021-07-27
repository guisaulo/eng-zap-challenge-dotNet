using System.Collections;

namespace Challenge.RealStates.Infrastructure.Data
{
    public class DataInMemory
    {
        public DataSet Zap { get; set; }
        public DataSet VivaReal { get; set; }

        public void InstantiateDataInMemory()
        {
            this.Zap = new DataSet
            {
                Data = new Hashtable(),
                Filters = new DataFilter()
            };

            this.VivaReal = new DataSet
            {
                Data = new Hashtable(),
                Filters = new DataFilter()
            };
        }
    }
}