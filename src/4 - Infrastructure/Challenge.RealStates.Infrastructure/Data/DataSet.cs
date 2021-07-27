using System.Collections.Generic;

namespace Challenge.RealStates.Infrastructure.Data
{
    public class DataSet
    {
        public List<string> DataIds { get; set; }
        public Filters Filters { get; set; }
    }
}