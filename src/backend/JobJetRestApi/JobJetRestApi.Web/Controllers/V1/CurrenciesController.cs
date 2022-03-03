using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Currency.Commands;
using JobJetRestApi.Application.UseCases.Currency.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrencyQueries _currencyQueries;
        private readonly IPageUriService _pageUriService;
        
        public CurrenciesController(IMediator mediator,
            IPageUriService pageUriService,
            ICurrencyQueries currencyQueries)
        {
            _currencyQueries = Guard.Against.Null(currencyQueries, nameof(currencyQueries));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }
        
        // GET api/currencies
        [HttpGet(ApiRoutes.Currencies.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<CurrencyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CurrencyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CurrencyResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var currencies = await _currencyQueries.GetAllCurrenciesAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<CurrencyResponse>.CreatePagedResponse(
                currencies.ToList(), "", true, null, filter, 666, _pageUriService, route));
        }
        
        // GET api/currencies/5
        [HttpGet(ApiRoutes.Currencies.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CurrencyResponse>> Get(int id)
        {
            var query = new GetCurrencyByIdQuery(id);

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (CurrencyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/currencies
        [HttpPost(ApiRoutes.Currencies.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateCurrencyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateCurrencyCommand(request.Name, request.IsoCode, request.IsoNumber);

            try
            {
                var currencyId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { Id = currencyId });
            }
            catch (CurrencyAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/currencies/5
        [HttpPut(ApiRoutes.Currencies.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCurrencyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateCurrencyCommand(id, request.Name);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (CurrencyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (CurrencyAlreadyExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/currencies/5
        [HttpDelete(ApiRoutes.Currencies.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCurrencyCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (CurrencyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}