using System.Linq;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Profiles.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace JobJetRestApi.Web.Controllers.V1;
[ApiController]
public class ProfilesController : ControllerBase
{
    private readonly IProfileQueries _profileQueries;

    public ProfilesController(IProfileQueries profileQueries)
    {
        _profileQueries = profileQueries;
    }

    // GET api/profiles/me
    [HttpGet(ApiRoutes.Profiles.MyProfile)]
    [ProducesResponseType(typeof(MyProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MyProfileResponse>> Get()
    {
        var currentUserId = int.Parse(this.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
        var response = await _profileQueries.GetMyProfile(currentUserId);
        
        return Ok(response);
    }
}