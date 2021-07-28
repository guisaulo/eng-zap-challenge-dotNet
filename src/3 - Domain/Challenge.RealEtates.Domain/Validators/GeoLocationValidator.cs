using Challenge.RealEtates.Domain.Entities;
using FluentValidation;

namespace Challenge.RealEtates.Domain.Validators
{
    public class GeoLocationValidator : AbstractValidator<GeoLocation>
    {
        public GeoLocationValidator()
        {
            RuleFor(m => m.Precision).NotEmpty().NotNull();
            RuleFor(x => x.Location)
                .NotNull()
                .SetValidator(new LocationValidator())
                .When(x => x.Location != null);
        }
    }
}