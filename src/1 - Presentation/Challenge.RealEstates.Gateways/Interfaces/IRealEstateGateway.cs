using Challenge.RealEstates.Domain.Entities;
using System.Collections.Generic;

namespace Challenge.RealEstates.Gateways.Interfaces
{
    public interface IRealEstateGateway
    {
        IEnumerable<RealEstate>GetRealEstatesFromSourceUrl(string url);
    }
}