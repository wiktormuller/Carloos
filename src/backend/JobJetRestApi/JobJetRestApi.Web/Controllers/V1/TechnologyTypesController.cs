using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.TechnologyType.Commands;
using JobJetRestApi.Application.UseCases.TechnologyType.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class TechnologyTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITechnologyTypeQueries _technologyTypeQueries;
        private readonly IPageUriService _pageUriService;
        
        public TechnologyTypesController(IMediator mediator, 
            IPageUriService pageUriService, 
            ITechnologyTypeQueries technologyTypeQueries)
        {
            _technologyTypeQueries = Guard.Against.Null(technologyTypeQueries, nameof(technologyTypeQueries));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }
        
        // GET api/technology-types
        [HttpGet(ApiRoutes.TechnologyTypes.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<TechnologyTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<TechnologyTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TechnologyTypeResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var technologyTypes = await _technologyTypeQueries.GetAllTechnologyTypesAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<TechnologyTypeResponse>.CreatePagedResponse(
                technologyTypes.ToList(), "", true, null, filter, 666, _pageUriService, route));
        }
        
        // GET api/technology-types/5
        [HttpGet(ApiRoutes.TechnologyTypes.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TechnologyTypeResponse>> Get(int id)
        {
            try
            {
                var result = await _technologyTypeQueries.GetTechnologyTypeByIdAsync(id);
                return Ok(result);
            }
            catch (TechnologyTypeNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/technology-types
        [HttpPost(ApiRoutes.TechnologyTypes.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateTechnologyTypeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateTechnologyTypeCommand(request.Name);

            try
            {
                var technologyTypeId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { Id = technologyTypeId });
            }
            catch (TechnologyTypeAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/technology-types/5
        [HttpPut(ApiRoutes.TechnologyTypes.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateTechnologyTypeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateTechnologyTypeCommand(id, request.Name);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (TechnologyTypeNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (TechnologyTypeAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/technology-types/5
        [HttpDelete(ApiRoutes.TechnologyTypes.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteTechnologyTypeCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (TechnologyTypeNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}