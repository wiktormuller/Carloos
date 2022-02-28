using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Contracts.V1.Responses;
using JobJetRestApi.Application.Interfaces;
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

        /// <exception cref="ArgumentException"></exception>
        public async Task<List<RoadResponse>> Handle(GetRoadBetweenCoordinatesQuery request, CancellationToken cancellationToken)
        {
            var indexOfSemicolon = request.Coordinates.IndexOf(';');
            if (indexOfSemicolon == -1)
            {
                throw new ArgumentException();
                //return IncorrectDelimiterOfCoordinatesException.ForCoordinates(request.Coordinates);
            }

            var firstCoordinatesPair = request.Coordinates.Substring(0, indexOfSemicolon);
            var secondCoordinatesPair = request.Coordinates.Substring(indexOfSemicolon + 1);

            var firstCoordinateTuple = GetCoordinatesFromPairString(firstCoordinatesPair, ',');
            var secondCoordinateTuple = GetCoordinatesFromPairString(secondCoordinatesPair, ',');
            
            var firstGeoPoint = new GeoPoint(firstCoordinateTuple.longitude, firstCoordinateTuple.latitude);
            var secondGeoPoint = new GeoPoint(secondCoordinateTuple.longitude, secondCoordinateTuple.latitude);

            var pointsBetweenTwoGeoPoints = await _routeService.GetPointsBetweenTwoGeoPoints(firstGeoPoint, secondGeoPoint);

            var result = pointsBetweenTwoGeoPoints
                .Select(geoPoint => new RoadResponse
                {
                    Longitude = geoPoint.Longitude, 
                    Latitude = geoPoint.Latitude
                }).ToList();

            return result;
        }

        private (decimal longitude, decimal latitude) GetCoordinatesFromPairString(string coordinatesPair, char delimiter)
        {
            var indexOfDelimiter = coordinatesPair.IndexOf(delimiter);

            var longitude = coordinatesPair.Substring(0, indexOfDelimiter);
            var latitude = coordinatesPair.Substring(indexOfDelimiter + 1);

            var longitudeAsDecimal = decimal.Parse(longitude, CultureInfo.InvariantCulture);
            var latitudeAsDecimal = decimal.Parse(latitude, CultureInfo.InvariantCulture);
            
            var coordinatesTuple = (longitudeAsDecimal, latitudeAsDecimal);

            return coordinatesTuple;
        }
    }
}