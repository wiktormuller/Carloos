using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Infrastructure.Validators
{
    public class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        public string Town { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int CountryIsoId { get; set; }

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
        }
    }
}