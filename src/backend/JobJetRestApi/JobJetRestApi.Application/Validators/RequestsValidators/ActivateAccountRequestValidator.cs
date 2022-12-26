using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators;

public class ActivateAccountRequestValidator : AbstractValidator<ActivateAccountRequest>
{
    public ActivateAccountRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(200);

        RuleFor(request => request.Token)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);
    }
}