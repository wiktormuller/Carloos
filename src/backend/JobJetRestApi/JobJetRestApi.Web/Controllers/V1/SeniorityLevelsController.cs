using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IPageUriService _pageUriService;
        
        public SeniorityLevelsController(IMediator mediator, 
            IPageUriService pageUriService)
        {
            _mediator = mediator;
            _pageUriService = pageUriService;
        }
        
        // GET api/seniority-levels
        [HttpGet(ApiRoutes.SeniorityLevels.GetAll)]
        //[ProducesResponseType(typeof(PagedResponse<SeniorityLevelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<SeniorityLevelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<IEnumerable<SeniorityLevelResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // @TODO - Pagination and filtering?
            //var route = Request.Path.Value;
            //var totalRecords = 100;
            //var data = new List<SeniorityLevelResponse>();

            //return Ok(PagedResponse<SeniorityLevelResponse>.CreatePagedResponse(data, filter, totalRecords,
            //    _pageUriService, route));
            
            var query = new GetAllSeniorityLevelsQuery();
            
            return Ok(await _mediator.Send(query));
        }

        // GET api/seniority-levels/5
        [HttpGet(ApiRoutes.SeniorityLevels.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeniorityLevelResponse>> Get(int id)
        {
            var query = new GetSeniorityLevelByIdQuery(id);

            try
            {
                var result = await _mediator.Send(query);
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