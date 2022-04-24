using System;
using System.Linq;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Auth.Commands;
using JobJetRestApi.Application.UseCases.Users.Commands;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1;

[ApiController]
public class AuthController : Controller
{
    private readonly IMediator _mediator;
    
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route(ApiRoutes.Auth.Identity)]
    [HttpGet]
    public IActionResult Get()
    {
        return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
    }

    [Route(ApiRoutes.Auth.Register)]
    [HttpPost]
    public async Task<ActionResult<int>> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new CreateUserCommand(request.Name, request.Email, request.Password);

        try
        {
            var userId = await _mediator.Send(command);
            return Created(nameof(UsersController.Get),new {Id = userId});
        }
        catch (Exception exception) when (exception is UserAlreadyExistsException)
        {
            return BadRequest(exception.Message);
        }
    }

    [Route(ApiRoutes.Auth.Login)]
    [HttpPost]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new LoginCommand(request.Email, request.Password);

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}