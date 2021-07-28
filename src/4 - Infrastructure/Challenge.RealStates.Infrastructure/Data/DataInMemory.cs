﻿using Challenge.RealStates.Infrastructure.Data.Interfaces;
using System.Collections;

namespace Challenge.RealStates.Infrastructure.Data
{
    public class DataInMemory : IDataInMemory
    {
        public Hashtable Data { get; set; }
        public DataSet Zap { get; set; }
        public DataSet VivaReal { get; set; }
    }
}