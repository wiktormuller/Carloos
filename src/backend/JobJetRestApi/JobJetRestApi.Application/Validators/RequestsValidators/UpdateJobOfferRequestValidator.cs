using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Application.Validators.RequestsValidators
{
    public class UpdateJobOfferRequestValidator : AbstractValidator<UpdateJobOfferRequest>
    {
        public UpdateJobOfferRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotNull()
                .Length(1, 200);
            
            RuleFor(request => request.Description)
                .NotNull()
                .Length(1, 4000);

            // Max: 9999999999999999,99
            // Scale refers to maximum number of decimal places
            // Precision refers to maximum number of digits that are present in the number
            RuleFor(request => request.SalaryFrom)
                .GreaterThan(0)
                .ScalePrecision(2, 18); 
                                        
            RuleFor(request => request.SalaryTo)
                .GreaterThan(0)
                .ScalePrecision(2, 18);
        }
    }
}