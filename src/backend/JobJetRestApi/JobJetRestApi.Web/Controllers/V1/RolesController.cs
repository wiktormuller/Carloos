using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Roles.Commands;
using JobJetRestApi.Application.UseCases.Roles.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1;

[ApiController]
public class RolesController : ControllerBase
{
    private readonly IPageUriService _pageUriService;
    private readonly IMediator _mediator;
    private readonly IRoleQueries _roleQueries;
    
    public RolesController(IPageUriService pageUriService, 
        IMediator mediator, 
        IRoleQueries roleQueries)
    {
        _pageUriService = pageUriService;
        _mediator = mediator;
        _roleQueries = roleQueries;
    }
    
    // GET api/roles
    [HttpGet(ApiRoutes.Roles.GetAll)]
    [ProducesResponseType(typeof(PagedResponse<RoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<RoleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RoleResponse>>> Get([FromQuery] PaginationFilter filter)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var roles = await _roleQueries.GetAllRolesAsync(filter);
            
        var route = Request.Path.Value;

        return Ok(PagedResponse<RoleResponse>.CreatePagedResponse(
            roles.ToList(), "", true, null, filter, 666, _pageUriService, route));
    }
    
    // GET api/roles/5
    [HttpGet(ApiRoutes.Roles.Get)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleResponse>> Get(int id)
    {
        try
        {
            var result = await _roleQueries.GetRoleByIdAsync(id);
            return Ok(result);
        }
        catch (RoleNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    // POST api/roles
    [HttpPost(ApiRoutes.Roles.Create)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post([FromBody] CreateRoleRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new CreateRoleCommand(request.Name);

        try
        {
            var roleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new {Id = roleId});
        }
        catch (RoleAlreadyExistsException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    
    // POST api/roles/5/users/5
    [HttpPost(ApiRoutes.Roles.AssignToUser)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> Post(int id, int userId)
    {
        var command = new AssignRoleToUserCommand(id, userId);

        try
        {
            var userRoleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new {UserRoleId = userRoleId});
        }
        catch (RoleNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (UserNotFoundException exception)
        {
            return NotFound(exception.Message);
        }
    }
}