using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(request => request.Email)
                .NotNull()
                .Length(1, 200);

            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 200);

            RuleFor(request => request.Password)
                .NotNull()
                .Length(1, 100);
        }
    }
}