using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPageUriService _pageUriService;
        
        public CountriesController(IMediator mediator, 
            IPageUriService pageUriService)
        {
            _mediator = mediator;
            _pageUriService = pageUriService;
        }
        
        // GET api/countries
        [HttpGet(ApiRoutes.Countries.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<CountryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<PagedResponse<CountryResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // @TODO - Pagination and filtering?
            var route = Request.Path.Value;
            var totalRecords = 100;
            var data = new List<CountryResponse>();

            return Ok(PagedResponse<CountryResponse>.CreatePagedResponse(data, filter, totalRecords, _pageUriService,
                route));
        }
        
        // GET api/countries/5
        [HttpGet(ApiRoutes.Countries.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryResponse>> Get(int id)
        {
            var query = new GetCountryByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        
        // POST api/countries
        [HttpPost(ApiRoutes.Countries.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateCountryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateCountryCommand(request.Name, request.Alpha2Code, request.Alpha3Code, request.NumericCode);

            var countryId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new {Id = countryId});
        }
        
        // PUT api/countries/5
        [HttpPut(ApiRoutes.Countries.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCountryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var command = new UpdateCountryCommand(id, request.Name);

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
        
        // DELETE api/countries/5
        [HttpDelete(ApiRoutes.Countries.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCountryCommand(id);

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