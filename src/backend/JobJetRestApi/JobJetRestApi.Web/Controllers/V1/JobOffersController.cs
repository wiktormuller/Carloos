using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Filters;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobJetRestApi.Application.Exceptions;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPageUriService _pageUriService;

        public JobOffersController(IMediator mediator, 
            IPageUriService pageUriService)
        {
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }

        // GET api/job-offers
        [HttpGet(ApiRoutes.JobOffers.GetAll)]
        //[ProducesResponseType(typeof(PagedResponse<JobOfferResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<JobOfferResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<IEnumerable<JobOfferResponse>>> Get([FromQuery] PaginationFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // @TODO - Pagination and filtering?
            //var route = Request.Path.Value;
            //var totalRecords = 100;
            //var data = new List<JobOfferResponse>();

            //return Ok(PagedResponse<JobOfferResponse>.CreatePagedResponse(data, filter, totalRecords, _pageUriService, 
            //    route));
            
            var query = new GetAllJobOffersQuery();
            
            return Ok(await _mediator.Send(query));
        }
        
        // GET api/job-offers/5
        [HttpGet(ApiRoutes.JobOffers.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobOfferResponse>> Get(int id)
        {
            var query = new GetJobOfferByIdQuery(id);

            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (JobOfferNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/job-offers
        [HttpPost(ApiRoutes.JobOffers.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateJobOfferRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateJobOfferCommand
            (
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo,
                request.TechnologyTypeId,
                request.SeniorityId,
                request.EmploymentTypeId,
                request.Address.Town,
                request.Address.Street,
                request.Address.ZipCode,
                request.Address.CountryIsoId,
                request.CurrencyId
            );

            try
            {
                var jobOfferId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { Id = jobOfferId });
            }
            catch (Exception e) when (e is SeniorityLevelNotFoundException
                || e is TechnologyTypeNotFoundException
                || e is EmploymentTypeNotFoundException
                || e is CountryNotFoundException
                || e is CurrencyNotFoundException
                || e is InvalidAddressException)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/job-offers/5
        [HttpPut(ApiRoutes.JobOffers.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateJobOfferRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateJobOfferCommand
            (
                id,
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo
            );

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (JobOfferNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // DELETE api/job-offers/5
        [HttpDelete(ApiRoutes.JobOffers.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteJobOfferCommand(id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (JobOfferNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}