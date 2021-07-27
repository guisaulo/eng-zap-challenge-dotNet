using Challenge.RealEstates.Gateways.Interfaces;
using Challenge.RealEtates.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace Challenge.RealEstates.Gateways
{
    public class RealEstateGateway : IRealEstateGateway
    {
        public IEnumerable<RealEstate> GetRealEstatesFromSourceURL(string sourceURL)
        {
            var json = string.Empty;
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(sourceURL);
            }

            var realEstates = JsonConvert.DeserializeObject<List<RealEstate>>(json);
            return realEstates;
        }
    }
}
