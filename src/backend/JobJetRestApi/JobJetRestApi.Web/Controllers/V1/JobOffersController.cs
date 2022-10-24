using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
using JobJetRestApi.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJobOfferQueries _jobOfferQueries;
        private readonly IPageUriService _pageUriService;

        public JobOffersController(IMediator mediator, 
            IPageUriService pageUriService, 
            IJobOfferQueries jobOfferQueries)
        {
            _jobOfferQueries = Guard.Against.Null(jobOfferQueries, nameof(jobOfferQueries));
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
            _pageUriService = Guard.Against.Null(pageUriService, nameof(pageUriService));
        }

        // GET api/job-offers
        [HttpGet(ApiRoutes.JobOffers.GetAll)]
        [ProducesResponseType(typeof(PagedResponse<JobOfferResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<JobOfferResponse>>> Get([FromQuery] JobOffersFilter filter) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var response = await _jobOfferQueries.GetAllJobOffersAsync(filter);
            
            var route = Request.Path.Value;

            return Ok(PagedResponse<JobOfferResponse>.CreatePagedResponse(
                response.JobOffers.ToList(), 
                "", 
                true,
                null, 
                filter, 
                response.TotalCount,
                _pageUriService, 
                route));
        }
        
        // GET api/job-offers/5
        [HttpGet(ApiRoutes.JobOffers.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JobOfferResponse>> Get(int id)
        {
            try
            {
                var result = await _jobOfferQueries.GetJobOfferByIdAsync(id);
                return Ok(result);
            }
            catch (JobOfferNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        // POST api/job-offers
        [Authorize(Roles = "User")]
        [HttpPost(ApiRoutes.JobOffers.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateJobOfferRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserId = int.Parse(this.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);

            var command = new CreateJobOfferCommand
            (
                currentUserId,
                request.CompanyId,
                request.Name,
                request.Description,
                request.SalaryFrom,
                request.SalaryTo,
                request.TechnologyTypeIds,
                request.SeniorityId,
                request.EmploymentTypeId,
                request.Address.Town,
                request.Address.Street,
                request.Address.ZipCode,
                request.Address.CountryIsoId,
                request.CurrencyId,
                request.WorkSpecification
            );

            try
            {
                var jobOfferId = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { Id = jobOfferId });
            }
            catch (Exception e) when (e is SeniorityLevelNotFoundException
                or TechnologyTypeNotFoundException
                or EmploymentTypeNotFoundException
                or CountryNotFoundException
                or CurrencyNotFoundException
                or InvalidAddressException
                or CannotCreateJobOfferException
                or UserCannotHaveCompaniesWithTheSameNamesException
                or CompanyNotFoundException)
            {
                return BadRequest(e.Message);
            }
        }
        
        // PUT api/job-offers/5
        [Authorize(Roles = "User")]
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
            
            var currentUserId = int.Parse(this.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);

            var command = new UpdateJobOfferCommand
            (
                currentUserId,
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
            catch (Exception e) when (e is CannotUpdateJobOfferException)
            {
                return BadRequest(e.Message);
            }
        }
        
        // DELETE api/job-offers/5
        [Authorize(Roles = "User")]
        [HttpDelete(ApiRoutes.JobOffers.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var currentUserId = int.Parse(this.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
            
            var command = new DeleteJobOfferCommand(currentUserId, id);

            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (JobOfferNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (CannotDeleteJobOfferException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}