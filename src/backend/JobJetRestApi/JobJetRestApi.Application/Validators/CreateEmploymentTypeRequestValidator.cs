using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators
{
    public class CreateEmploymentTypeRequestValidator : AbstractValidator<CreateEmploymentTypeRequest>
    {
        public CreateEmploymentTypeRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}