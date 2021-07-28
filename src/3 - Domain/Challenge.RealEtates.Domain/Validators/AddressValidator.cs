using Challenge.RealEtates.Domain.Entities;
using FluentValidation;

namespace Challenge.RealEtates.Domain.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(m => m.City).NotNull();
            RuleFor(m => m.Neighborhood).NotNull();
            RuleFor(x => x.GeoLocation)
                .NotNull()
                .SetValidator(new GeoLocationValidator())
                .When(x => x.GeoLocation != null);
        }
    }
}