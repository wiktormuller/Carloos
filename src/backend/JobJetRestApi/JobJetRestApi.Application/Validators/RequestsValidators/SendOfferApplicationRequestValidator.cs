using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Validators.Common;

namespace JobJetRestApi.Application.Validators.RequestsValidators;

public class SendOfferApplicationRequestValidator : AbstractValidator<SendOfferApplicationRequest>
{
    public SendOfferApplicationRequestValidator()
    {
        RuleFor(request => request.UserEmail)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(request => request.PhoneNumber)
            .NotEmpty()
            .Length(3, 20);

        RuleFor(request => request.File)
            .SetValidator(new FormFileValidator(10, 5242880, new PermittedExtensionsForOfferApplication()));
    }
}
