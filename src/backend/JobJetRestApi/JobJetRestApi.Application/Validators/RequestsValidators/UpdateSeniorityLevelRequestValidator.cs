using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class UpdateSeniorityLevelRequestValidator : AbstractValidator<UpdateSeniorityLevelRequest>
    {
        public UpdateSeniorityLevelRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}