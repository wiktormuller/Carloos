using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
    {
        public UpdateCompanyRequestValidator()
        {
            RuleFor(request => request.Description)
                .NotNull()
                .Length(1, 10_000);

            RuleFor(request => request.NumberOfPeople)
                .InclusiveBetween(1, 1_000_000);;
        }
    }
}