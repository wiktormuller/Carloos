using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class CreateCountryRequestValidator : AbstractValidator<CreateCountryRequest>
    {
        public CreateCountryRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 200);

            RuleFor(request => request.Alpha2Code)
                .NotNull()
                .Length(2);

            RuleFor(request => request.Alpha3Code)
                .NotNull()
                .Length(3);

            RuleFor(request => request.NumericCode)
                .InclusiveBetween(1, 1000);
            
            // @TODO - Add validation for coords
        }
    }
}