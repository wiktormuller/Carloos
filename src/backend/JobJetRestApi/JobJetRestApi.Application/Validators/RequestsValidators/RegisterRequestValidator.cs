using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .MinimumLength(5);
        
        RuleFor(request => request.Name)
            .NotEmpty()
            .MinimumLength(3);
        
        RuleFor(request => request.Password)
            .MinimumLength(8)
            .MaximumLength(100)
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}