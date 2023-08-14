using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Application.DTO;

namespace JobJetRestApi.Application.Ports
{
    public interface IRouteService
    {
        Task<List<GeoPointDto>> GetPointsBetweenTwoGeoPointsAsync(GeoPointDto firstGeoPoint, GeoPointDto secondGeoPoint);
    }
}