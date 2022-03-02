using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class CreateSeniorityLevelRequestValidator : AbstractValidator<CreateSeniorityLevelRequest>
    {
        public CreateSeniorityLevelRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}