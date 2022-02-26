using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators
{
    public class UpdateCurrencyRequestValidator : AbstractValidator<UpdateCurrencyRequest>
    {
        public UpdateCurrencyRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}