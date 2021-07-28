using Challenge.RealEtates.Domain.Entities;

namespace Challenge.RealEtates.Core.Interfaces.Services
{
    public interface IRealEstateValidationService
    {
        bool IsRealEstateInputValid(RealEstate realEstate);

        bool IsEligibleToZapPortal(RealEstate realEtate);

        bool IsEligibleToVivaRealPortal(RealEstate realEtate);
    }
}