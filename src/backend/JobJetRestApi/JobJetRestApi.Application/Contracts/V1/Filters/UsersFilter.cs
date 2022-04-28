using JetBrains.Annotations;

namespace JobJetRestApi.Application.Contracts.V1.Filters;

public class UsersFilter : PaginationFilter
{
    public int? CountryId { get; set; }
    public int? TechnologyId { get; set; }
    public int? SeniorityLevelId { get; set; }
    public int? EmploymentTypeId { get; set; }
    /// <summary>
    /// FullyRemote,
    /// Hybrid,
    /// Office
    /// </summary>
    [CanBeNull] public string WorkSpecification { get; set; }
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    [CanBeNull] public string GeneralSearchByText { get; set; }
}