using Challenge.RealEstates.Domain.Entities;
using FluentValidation;

namespace Challenge.RealEstates.Domain.Validators
{
    public class RealEstateValidator : AbstractValidator<RealEstate>
    {
        public RealEstateValidator()
        {
            RuleFor(m => m.Id).NotEmpty().NotNull();
            RuleFor(m => m.UsableAreas).NotNull();
            RuleFor(m => m.ListingStatus).NotEmpty().NotNull();
            RuleFor(m => m.Bathrooms).NotNull();
            RuleFor(m => m.Bedrooms).NotNull();
            RuleFor(m => m.ListingType).NotEmpty().NotNull();
            RuleFor(m => m.CreatedAt).NotEmpty().NotNull();
            RuleFor(m => m.ParkingSpaces).NotNull();
            RuleFor(m => m.UpdatedAt).NotEmpty().NotNull();
            RuleFor(m => m.Owner).NotNull();
            RuleFor(x => x.Images).NotNull();
            RuleFor(x => x.Address)
                .NotNull()
                .SetValidator(new AddressValidator())
                .When(x => x.Address != null);
            RuleFor(x => x.PricingInfos)
                .NotNull()
                .SetValidator(new PricingInfosValidator())
                .When(x => x.PricingInfos != null);

        }
    }
}
