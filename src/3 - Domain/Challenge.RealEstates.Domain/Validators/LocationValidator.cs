using Challenge.RealEstates.Domain.Entities;
using FluentValidation;

namespace Challenge.RealEstates.Domain.Validators
{
    public class LocationValidator : AbstractValidator<Location>
    {
        public LocationValidator()
        {
            RuleFor(m => m.Lat).NotNull().NotEqual(0);
            RuleFor(m => m.Lon).NotNull().NotEqual(0);
        }
    }
}