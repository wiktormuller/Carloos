using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Requests;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Exceptions;
using JobJetRestApi.Application.UseCases.Roads.Queries;
using JobJetRestApi.Web.Contracts.V1.ApiRoutes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers.V1
{
    public class RoadsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public RoadsController(IMediator mediator)
        {
            _mediator = Guard.Against.Null(mediator, nameof(mediator));
        }
        
        // GET api/roads/{coordinates}
        [HttpGet(ApiRoutes.Roads.Get)]
        [ProducesResponseType(typeof(List<RoadResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<RoadResponse>>> Get([FromQuery] GetRoadRequest request)
        {
            var query = new GetRoadBetweenCoordinatesQuery(
                request.SourceLongitude, 
                request.SourceLatitude, 
                request.DestinationLongitude, 
                request.DestinationLatitude);
                
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}