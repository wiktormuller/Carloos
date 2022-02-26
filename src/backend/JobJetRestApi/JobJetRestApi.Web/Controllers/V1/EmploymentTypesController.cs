using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IPageUriService _pageUriService;
        
        public EmploymentTypesController(IMediator mediator, IPageUriService pageUriService)
        {
            _mediator = mediator;
            _pageUriService = pageUriService;
        }
        
        // GET api/employment-types
        [HttpGet(ApiRoutes.EmploymentTypes.GetAll)]
        //[ProducesResponseType(typeof(PagedResponse<EmploymentTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<EmploymentTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<IEnumerable<EmploymentTypeResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // @TODO - Pagination and filtering?
            //var route = Request.Path.Value;
            //var totalRecords = 100;
            //var data = new List<EmploymentTypeResponse>();

            //return Ok(PagedResponse<EmploymentTypeResponse>.CreatePagedResponse(data, filter, totalRecords,
            //    _pageUriService, route));
            
            var query = new GetAllEmploymentTypesQuery();
            
            return Ok(await _mediator.Send(query));
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