using System.Collections.Generic;
using System.Threading.Tasks;
using JobJetRestApi.Domain.Entities;

namespace JobJetRestApi.Application.Interfaces
{
    public interface IRouteService
    {
        Task<List<GeoPoint>> GetPointsBetweenTwoGeoPoints(GeoPoint firstGeoPoint, GeoPoint secondGeoPoint);
    }
}