using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.EmploymentType.Commands;
using JobJetRestApi.Application.UseCases.EmploymentType.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class EmploymentTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmploymentTypeQueries _employmentTypeQueries;
        private readonly IPageUriService _pageUriService;
        
        public EmploymentTypesController(IMediator mediator, IPageUriService pageUriService,
            IEmploymentTypeQueries employmentTypeQueries)
        {
            _employmentTypeQueries = Guard.Against.Null(employmentTypeQueries, nameof(employmentTypeQueries));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }
        
        // GET api/employment-types
        [HttpGet(ApiRoutes.EmploymentTypes.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<EmploymentTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<EmploymentTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmploymentTypeResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var employmentTypes = await _employmentTypeQueries.GetAllEmploymentTypesAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<EmploymentTypeResponse>.CreatePagedResponse(
                employmentTypes.ToList(), "", true, null, filter, 666, _pageUriService, route));
        }
        
        // GET api/employment-types/5
        [HttpGet(ApiRoutes.EmploymentTypes.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmploymentTypeResponse>> Get(int id)
        {
            var query = new GetEmploymentTypeByIdQuery(id);

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (EmploymentTypeNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/employment-types
        [HttpPost(ApiRoutes.EmploymentTypes.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateEmploymentTypeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateEmploymentTypeCommand(request.Name);

            try
            {
                var employmentTypeId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new {Id = employmentTypeId});
            }
            catch (EmploymentTypeAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/employment-types/5
        [HttpPut(ApiRoutes.EmploymentTypes.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateEmploymentTypeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateEmploymentTypeCommand(id, request.Name);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (EmploymentTypeNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EmploymentTypeAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/employment-types/5
        [HttpDelete(ApiRoutes.EmploymentTypes.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteEmploymentTypeCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (EmploymentTypeNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}