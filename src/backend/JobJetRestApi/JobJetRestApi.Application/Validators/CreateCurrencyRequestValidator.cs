using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators
{
    public class CreateCurrencyRequestValidator : AbstractValidator<CreateCurrencyRequest>
    {
        public CreateCurrencyRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);

            RuleFor(request => request.IsoCode)
                .NotNull()
                .Length(3);

            RuleFor(request => request.IsoNumber)
                .InclusiveBetween(1, 1000);
        }
    }
}