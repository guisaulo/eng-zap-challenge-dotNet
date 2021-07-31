using Challenge.RealEstates.Gateways.Interfaces;
using Challenge.RealEstates.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Challenge.RealEstates.Gateways
{
    public class RealEstateGateway : IRealEstateGateway
    {
        public IEnumerable<RealEstate> GetRealEstatesFromSourceUrl(string url)
        {
            string json;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }

            var realEstates = JsonConvert.DeserializeObject<List<RealEstate>>(json);
            return realEstates;
        }
    }
}
