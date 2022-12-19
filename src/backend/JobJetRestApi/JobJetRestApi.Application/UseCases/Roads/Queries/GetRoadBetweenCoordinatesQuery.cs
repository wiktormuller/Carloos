using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Roads.Queries
{
    public class GetRoadBetweenCoordinatesQuery : IRequest<List<RoadResponse>>
    {
        public decimal SourceLongitude { get; }
        public decimal SourceLatitude { get; }
        public decimal DestinationLongitude { get; }
        public decimal DestinationLatitude { get; }

        public GetRoadBetweenCoordinatesQuery(decimal sourceLongitude, 
            decimal sourceLatitude, 
            decimal destinationLongitude, 
            decimal destinationLatitude)
        {
            SourceLongitude = sourceLongitude;
            SourceLatitude = sourceLatitude;
            DestinationLongitude = destinationLongitude;
            DestinationLatitude = destinationLatitude;
        }
    }
}