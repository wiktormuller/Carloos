using System.Collections.Generic;
using JobJetRestApi.Application.Contracts.V1.Responses;
using MediatR;

namespace JobJetRestApi.Application.UseCases.Roads.Queries
{
    public class GetRoadBetweenCoordinatesQuery : IRequest<List<RoadResponse>>
    {
        public string Coordinates { get; }

        public GetRoadBetweenCoordinatesQuery(string coordinates)
        {
            Coordinates = coordinates;
        }
    }
}