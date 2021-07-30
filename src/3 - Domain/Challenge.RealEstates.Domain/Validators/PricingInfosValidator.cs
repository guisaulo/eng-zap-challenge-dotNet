using Challenge.RealEstates.Domain.Entities;
using FluentValidation;

namespace Challenge.RealEstates.Domain.Validators
{
    public class PricingInfosValidator : AbstractValidator<PricingInfos>
    {
        public PricingInfosValidator()
        {
            RuleFor(m => m.BusinessType).NotEmpty().NotNull();
            RuleFor(m => m.Price)
                .NotNull()
                .Custom((x, context) =>
                {
                    if (x < 0)
                    {
                        context.AddFailure($"{x} is not a valid number or less than 0");
                    }
                });
        }
    }
}
