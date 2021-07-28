using Challenge.RealEtates.Domain.Entities;
using FluentValidation;

namespace Challenge.RealEtates.Domain.Validators
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
                    if ((!(long.TryParse(x, out long value)) || value < 0))
                    {
                        context.AddFailure($"{x} is not a valid number or less than 0");
                    }
                });
            RuleFor(m => m.MonthlyCondoFee)
                .NotNull()
                .Custom((x, context) =>
                {
                    if ((!(long.TryParse(x, out long value)) || value < 0))
                    {
                        context.AddFailure($"{x} is not a valid number or less than 0");
                    }
                });
        }
    }
}
