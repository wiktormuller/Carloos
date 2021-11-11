using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
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
        private readonly IPageUriService _pageUriService;
        
        public TechnologyTypesController(IMediator mediator, 
            IPageUriService pageUriService)
        {
            _mediator = mediator;
            _pageUriService = pageUriService;
        }
        
        // GET api/technology-types
        [HttpGet(ApiRoutes.TechnologyTypes.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<TechnologyTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<PagedResponse<TechnologyTypeResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var route = Request.Path.Value;
            var totalRecords = 100;
            var data = new List<TechnologyTypeResponse>();

            return Ok(PagedResponse<TechnologyTypeResponse>.CreatePagedResponse(data, filter, totalRecords,
                _pageUriService, route));
        }
        
        // GET api/technology-types/5
        [HttpGet(ApiRoutes.TechnologyTypes.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Get(int id)
        {
            var query = new GetTechnologyTypeByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
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

            var command = new CreateTechnologyTypeCommand();

            var technologyTypeId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { Id = technologyTypeId });
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

            var command = new UpdateTechnologyTypeCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound();
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
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}