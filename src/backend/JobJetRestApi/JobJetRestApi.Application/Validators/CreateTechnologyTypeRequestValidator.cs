using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators
{
    public class CreateTechnologyTypeRequestValidator : AbstractValidator<CreateTechnologyTypeRequest>
    {
        public CreateTechnologyTypeRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}