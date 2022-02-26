using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators
{
    public class UpdateTechnologyTypeRequestValidator : AbstractValidator<UpdateTechnologyTypeRequest>
    {
        public UpdateTechnologyTypeRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 100);
        }
    }
}