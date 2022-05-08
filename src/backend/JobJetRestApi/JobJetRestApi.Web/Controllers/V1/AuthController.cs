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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    [Authorize]
    [Route(ApiRoutes.Auth.Identity)]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
    }

    [Route(ApiRoutes.Auth.Register)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new LoginCommand(request.Email, request.Password, GetIpAddressFromRequest());

        try
        {
            var response = await _mediator.Send(command);

            SetRefreshTokenCookieInRequest(response.RefreshToken);

            return Ok(response.LoginResponse);
        }
        catch (Exception exception) when (exception is AuthException)
        {
            return BadRequest(exception.Message);
        }
    }

    [Route(ApiRoutes.Auth.Refresh)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> RefreshTokens()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new RefreshCommand(GetRefreshTokenFromRequestCookie(), GetIpAddressFromRequest());

        try
        {
            var response = await _mediator.Send(command);
            
            SetRefreshTokenCookieInRequest(response.RefreshToken);

            return Ok(response.LoginResponse);
        }
        catch (Exception exception) when (exception is RefreshTokenIsMissedInRequestException
                                          || exception is CannotFindProperRefreshTokenForUserException
                                          || exception is RefreshTokenIsNotActiveException
                                          || exception is PassedRefreshTokenIsInvalidException)
        {
            return BadRequest(exception.Message);
        }
    }
    
    [Route(ApiRoutes.Auth.Revoke)]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult RevokeToken()
    {
        try
        {
            GetRefreshTokenFromRequestCookie();

            var command = new RevokeTokenCommand(GetRefreshTokenFromRequestCookie(), GetIpAddressFromRequest());

            _mediator.Send(command);
            
            return Ok();
        }
        catch (Exception exception) when (exception is RefreshTokenIsMissedInRequestException
                                          || exception is CannotFindProperRefreshTokenForUserException
                                          || exception is RefreshTokenIsNotActiveException
                                          || exception is PassedRefreshTokenIsInvalidException)
        {
            return BadRequest(exception.Message);
        }
    }
    
    // Helpers
    private string GetRefreshTokenFromRequestCookie()
    {
        return Request.Cookies["refreshToken"];
    }
    
    private void SetRefreshTokenCookieInRequest(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    private string GetIpAddressFromRequest()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return Request.Headers["X-Forwarded-For"];
        }
        else
        {
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}