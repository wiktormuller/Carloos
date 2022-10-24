using FluentValidation;
using JobJetRestApi.Application.Contracts.V1.Filters;

namespace JobJetRestApi.Application.Validators.FiltersValidators;

public class PaginationFilterValidator : AbstractValidator<PaginationFilter>
{
    public PaginationFilterValidator()
    {
        RuleFor(filter => filter.PageNumber)
            .GreaterThanOrEqualTo(0);

        RuleFor(filter => filter.PageSize)
            .InclusiveBetween(0, 100);
    }
}