using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Filters;

namespace JobJetRestApi.Application.Validators.FiltersValidators;

public class UsersFilterValidator : AbstractValidator<JobOffersFilter>
{
    public UsersFilterValidator()
    {
        RuleFor(filter => filter.CountryId)
            .GreaterThan(0);

        RuleFor(filter => filter.SalaryFrom)
            .GreaterThan(0);

        RuleFor(filter => filter.SalaryTo)
            .LessThan(int.MaxValue);

        RuleFor(filter => filter.TechnologyId)
            .GreaterThan(0);

        RuleFor(filter => filter.EmploymentTypeId)
            .GreaterThan(0);

        RuleFor(filter => filter.SeniorityLevelId)
            .GreaterThan(0);
    }
}