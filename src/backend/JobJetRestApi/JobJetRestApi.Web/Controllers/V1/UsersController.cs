using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Users.Commands;
using JobJetRestApi.Application.UseCases.Users.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPageUriService _pageUriService;
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;
        
        public UsersController(IMediator mediator, 
            IPageUriService pageUriService, 
            IUserQueries userQueries)
        {
            _userQueries = Guard.Against.Null(userQueries, nameof(userQueries));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
        }
        
        // GET api/users
        [HttpGet(ApiRoutes.Users.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<UserResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userQueries.GetAllUsersAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<UserResponse>.CreatePagedResponse(
                response.Users.ToList(), 
                "", 
                true, 
                null, 
                filter, 
                response.TotalCount, 
                _pageUriService, 
                route));
        }
        
        // GET api/users/5
        [HttpGet(ApiRoutes.Users.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponse>> Get(int id)
        {
            try
            {
                var result = await _userQueries.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // GET api/users/5/refresh-tokens
        [HttpGet(ApiRoutes.Users.GetRefreshTokens)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RefreshTokenResponse>> GetRefreshTokens(int id)
        {
            try
            {
                var result = await _userQueries.GetRefreshTokensAsync(id);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/users
        [HttpPost(ApiRoutes.Users.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateUserCommand(request.Email, request.Name, request.Password);

            try
            {
                var userId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new {Id = userId});
            }
            catch (UserAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/users/5
        [HttpPut(ApiRoutes.Users.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var command = new UpdateUserCommand(id, request.UserName);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UserAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/users/5
        [HttpDelete(ApiRoutes.Users.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUserCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}