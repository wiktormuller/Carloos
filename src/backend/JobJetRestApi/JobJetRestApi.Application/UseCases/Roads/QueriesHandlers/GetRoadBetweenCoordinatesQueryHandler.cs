using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Ports;
using JobJetRestApi.Application.UseCases.Roads.Queries;
using JobJetRestApi.Domain.Entities;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Roads.QueriesHandlers
{
    public class GetRoadBetweenCoordinatesQueryHandler : IRequestHandler<GetRoadBetweenCoordinatesQuery, List<RoadResponse>>
    {
        private readonly IRouteService _routeService;

        public GetRoadBetweenCoordinatesQueryHandler(IRouteService routeService)
        {
            _routeService = Guard.Against.Null(routeService, nameof(routeService));
        }
        
        public async Task<List<RoadResponse>> Handle(GetRoadBetweenCoordinatesQuery request, CancellationToken cancellationToken)
        {
            var firstGeoPoint = new GeoPoint(request.SourceLongitude, request.SourceLatitude);
            var secondGeoPoint = new GeoPoint(request.DestinationLongitude, request.DestinationLatitude);

            // @TODO: What if external service didn't response
            var pointsBetweenTwoGeoPoints = await _routeService.GetPointsBetweenTwoGeoPointsAsync(firstGeoPoint, secondGeoPoint);

            var result = pointsBetweenTwoGeoPoints
                .Select(geoPoint => new RoadResponse
                {
                    Longitude = geoPoint.Longitude, 
                    Latitude = geoPoint.Latitude
                }).ToList();

            return result;
        }
    }
}