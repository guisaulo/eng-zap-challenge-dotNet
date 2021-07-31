using Challenge.RealEstates.Domain.Entities;

namespace Challenge.RealEstates.Domain.Core.Interfaces.Services
{
    public interface IRealEstateValidationService
    {
        bool IsRealEstateInputValid(RealEstate realEstate);

        bool IsEligibleToZapPortal(RealEstate realEstate);

        bool IsEligibleToVivaRealPortal(RealEstate realEstate);
    }
}