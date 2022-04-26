using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Countries.Commands;
using JobJetRestApi.Application.UseCases.Countries.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICountryQueries _countryQueries;
        private readonly IPageUriService _pageUriService;
        
        public CountriesController(IMediator mediator, 
            IPageUriService pageUriService, 
            ICountryQueries countryQueries)
        {
            _countryQueries = Guard.Against.Null(countryQueries, nameof(countryQueries));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }
        
        // GET api/countries
        [HttpGet(ApiRoutes.Countries.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<CountryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CountryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CountryResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var countries = await _countryQueries.GetAllCountriesAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<CountryResponse>.CreatePagedResponse(
                countries.ToList(), "", true, null, filter, 666, _pageUriService, route));
        }
        
        // GET api/countries/5
        [HttpGet(ApiRoutes.Countries.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CountryResponse>> Get(int id)
        {
            try
            {
                var result = await _countryQueries.GetCountryByIdAsync(id);
                return Ok(result);
            }
            catch (CountryNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/countries
        [Authorize(Roles = "Administrator")]
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

            try
            {
                var countryId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new {Id = countryId});
            }
            catch (CountryAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/countries/5
        [Authorize(Roles = "Administrator")]
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
            catch (CountryNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (CountryAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/countries/5
        [Authorize(Roles = "Administrator")]
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
            catch (CountryNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}