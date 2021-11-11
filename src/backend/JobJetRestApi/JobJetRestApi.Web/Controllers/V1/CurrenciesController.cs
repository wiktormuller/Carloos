using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
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
        private readonly IPageUriService _pageUriService;
        
        public CurrenciesController(IMediator mediator,
            IPageUriService pageUriService)
        {
            _mediator = mediator;
            _pageUriService = pageUriService;
        }
        
        // GET api/currencies
        [HttpGet(ApiRoutes.Currencies.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<CurrencyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<PagedResponse<CurrencyResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var route = Request.Path.Value;
            var totalRecords = 100;
            var data = new List<CurrencyResponse>();

            return Ok(PagedResponse<CurrencyResponse>.CreatePagedResponse(data, filter, totalRecords,
                _pageUriService, route));
        }
        
        // GET api/currencies/5
        [HttpGet(ApiRoutes.Currencies.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Get(int id)
        {
            var query = new GetCurrencyByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
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

            var command = new CreateCurrencyCommand();

            var currencyId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { Id = currencyId });
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

            var command = new UpdateCurrencyCommand(id);

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
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}