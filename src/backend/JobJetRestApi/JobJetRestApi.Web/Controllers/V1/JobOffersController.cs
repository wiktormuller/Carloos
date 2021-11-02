using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.UseCases.JobOffers.Commands;
using JobJetRestApi.Application.UseCases.JobOffers.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    [ApiController]
    public class JobOffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobOffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/job-offers
        [HttpGet(ApiRoutes.JobOffers.GetAll)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // For filter validation
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return new string[] { "value1", "value2"};
        }
        
        // GET api/job-offers/5
        [HttpGet(ApiRoutes.JobOffers.Get)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Get(int id)
        {
            var query = new GetJobOfferByIdQuery(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        
        // POST api/job-offers
        [HttpPost(ApiRoutes.JobOffers.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Post([FromBody] CreateJobOfferRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
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
                request.Address.CountryIsoId
            );

            var jobOfferId = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { Id = jobOfferId });
        }
        
        // PUT api/job-offers/5
        [HttpPut(ApiRoutes.JobOffers.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Put(int id, [FromBody] UpdateJobOfferRequest request)
        {
            
        }
        
        // DELETE api/job-offers/5
        [HttpDelete(ApiRoutes.JobOffers.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete(int id)
        {
            
        }
    }
}