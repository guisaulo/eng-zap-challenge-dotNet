using Challenge.RealEtates.Domain.Entities;
using System.Collections.Generic;

namespace Challenge.RealEstates.Gateways.Interfaces
{
    public interface IRealEstateGateway
    {
        IEnumerable<RealEstate>GetRealEstatesFromSourceURL(string sourceURL);
    }
}