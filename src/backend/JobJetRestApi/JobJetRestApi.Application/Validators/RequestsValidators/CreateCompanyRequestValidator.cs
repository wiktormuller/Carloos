using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 200);

            RuleFor(request => request.ShortName)
                .NotNull()
                .Length(1, 50);

            RuleFor(request => request.Description)
                .NotNull()
                .Length(1, 10_000);

            RuleFor(request => request.NumberOfPeople)
                .InclusiveBetween(1, 1_000_000);

            RuleFor(request => request.CityName)
                .NotNull()
                .Length(1, 1000);
        }
    }
}