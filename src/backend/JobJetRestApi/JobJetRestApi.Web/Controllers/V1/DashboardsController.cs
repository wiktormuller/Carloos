using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Dashboards.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1;

[ApiController]
public class DashboardsController : ControllerBase
{
    private readonly IDashboardQueries _dashboardQueries;
    
    public DashboardsController(IDashboardQueries dashboardQueries)
    {
        _dashboardQueries = dashboardQueries;
    }
    
    [Route(ApiRoutes.Dashboards.GetAverageSalaryInCountries)]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AverageSalaryInCountryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AverageSalaryInCountryResponse>>> GetAverageSalaries()
    {
        var averageSalaries = await _dashboardQueries.GetAverageAndMedianSalariesInCountries();

        return Ok(averageSalaries);
    }
}