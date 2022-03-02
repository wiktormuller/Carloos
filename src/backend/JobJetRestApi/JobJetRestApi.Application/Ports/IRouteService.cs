using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Ports
{
    public interface IRouteService
    {
        Task<List<GeoPoint>> GetPointsBetweenTwoGeoPointsAsync(GeoPoint firstGeoPoint, GeoPoint secondGeoPoint);
    }
}