using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.UseCases.Companies.Commands;
using JobJetRestApi.Application.UseCases.Companies.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET api/companies
        [HttpGet(ApiRoutes.Companies.GetAll)]
        //[ProducesResponseType(typeof(PagedResponse<CountryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<CountryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<IEnumerable<CompanyResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // @TODO - Pagination and filtering?
            //var route = Request.Path.Value;
            //var totalRecords = 100;
            //var data = new List<CountryResponse>();

            //return Ok(PagedResponse<CountryResponse>.CreatePagedResponse(data, filter, totalRecords, _pageUriService,
            //    route));

            var query = new GetAllCompaniesQuery();
            
            return Ok(await _mediator.Send(query));
        }
        
        // GET api/companies/5
        [HttpGet(ApiRoutes.Companies.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyResponse>> Get(int id)
        {
            var query = new GetCompanyByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        
        // POST api/companies
        [HttpPost(ApiRoutes.Companies.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateCompanyCommand(request.Name, request.ShortName, request.Description, request.NumberOfPeople, request.CityName);

            var companyId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new {Id = companyId});
        }
        
        // PUT api/companies/5
        [HttpPut(ApiRoutes.Companies.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCompanyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var command = new UpdateCompanyCommand(id, request.Name, request.ShortName, request.Description, request.NumberOfPeople, request.CityName);

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
        
        // DELETE api/companies/5
        [HttpDelete(ApiRoutes.Companies.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCompanyCommand(id);

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