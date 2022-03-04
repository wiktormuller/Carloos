using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Commands;
using JobJetRestApi.Application.UseCases.SeniorityLevel.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class SeniorityLevelsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISeniorityLevelQueries _seniorityLevelQueries;
        private readonly IPageUriService _pageUriService;
        
        public SeniorityLevelsController(IMediator mediator, 
            IPageUriService pageUriService, 
            ISeniorityLevelQueries seniorityLevelQueries)
        {
            _seniorityLevelQueries = Guard.Against.Null(seniorityLevelQueries, nameof(seniorityLevelQueries));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }
        
        // GET api/seniority-levels
        [HttpGet(ApiRoutes.SeniorityLevels.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<SeniorityLevelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<SeniorityLevelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SeniorityLevelResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var seniorityLevels = await _seniorityLevelQueries.GetAllSeniorityLevelsAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<SeniorityLevelResponse>.CreatePagedResponse(
                seniorityLevels.ToList(), "", true, null, filter, 666, _pageUriService, route));
        }

        // GET api/seniority-levels/5
        [HttpGet(ApiRoutes.SeniorityLevels.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeniorityLevelResponse>> Get(int id)
        {
            try
            {
                var result = await _seniorityLevelQueries.GetSeniorityLevelByIdAsync(id);
                return Ok(result);
            }
            catch (SeniorityLevelNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/seniority-levels
        [HttpPost(ApiRoutes.SeniorityLevels.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateSeniorityLevelRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateSeniorityLevelCommand(request.Name);

            try
            {
                var seniorityLevelId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { Id = seniorityLevelId });
            }
            catch (SeniorityLevelAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/seniority-levels/5
        [HttpPut(ApiRoutes.SeniorityLevels.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateSeniorityLevelRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateSeniorityLevelCommand(id, request.Name);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (SeniorityLevelNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (SeniorityLevelAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/seniority-levels/5
        [HttpDelete(ApiRoutes.SeniorityLevels.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSeniorityLevelCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (EmploymentTypeAlreadyExistsException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}