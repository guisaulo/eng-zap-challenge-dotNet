using System.Collections.Generic;

namespace Challenge.RealStates.Infrastructure.Data
{
    public class Filters
    {
        public Dictionary<int, HashSet<string>> UsableAreas { get; set; }

        public Dictionary<int, HashSet<string>> ParkingSpaces { get; set; }

        public Dictionary<string, HashSet<string>> City { get; set; }

        public Dictionary<int, HashSet<string>> Bathrooms { get; set; }

        public Dictionary<int, HashSet<string>> Bedrooms { get; set; }

        public Dictionary<string, HashSet<string>> BusinessType { get; set; }

        public Dictionary<string, HashSet<string>> Price { get; set; }

        public Filters()
        {
            this.UsableAreas = new Dictionary<int, HashSet<string>>();
            this.ParkingSpaces = new Dictionary<int, HashSet<string>>();
            this.City = new Dictionary<string, HashSet<string>>();
            this.Bathrooms = new Dictionary<int, HashSet<string>>();
            this.Bedrooms = new Dictionary<int, HashSet<string>>();
            this.BusinessType = new Dictionary<string, HashSet<string>>();
            this.Price = new Dictionary<string, HashSet<string>>();
        }
    }
}