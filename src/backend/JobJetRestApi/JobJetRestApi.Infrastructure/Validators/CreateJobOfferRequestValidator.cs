using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Requests;

namespace JobJetRestApi.Infrastructure.Validators
{
    public class CreateJobOfferRequestValidator : AbstractValidator<CreateJobOfferRequest>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalaryFrom { get; set; }
        public decimal SalaryTo { get; set; }
        public CreateAddressRequest Address { get; set; }
        public int TechnologyTypeId { get; set; }
        public int SeniorityId { get; set; }
        public int EmploymentTypeId { get; set; }
        public CreateJobOfferRequestValidator()
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
                .NotEmpty()
                .ScalePrecision(2, 18); 
                                        
            RuleFor(request => request.SalaryTo)
                .NotEmpty()
                .ScalePrecision(2, 18);
        }
    }
}