using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        public CreateAddressRequestValidator()
        {
            RuleFor(request => request.Town)
                .NotNull()
                .Length(1, 200);
            
            RuleFor(request => request.Street)
                .NotNull()
                .Length(1, 300);
            
            RuleFor(request => request.ZipCode)
                .NotNull()
                .Length(1, 20);

            RuleFor(request => request.CountryId)
                .GreaterThan(0);
        }
    }
}