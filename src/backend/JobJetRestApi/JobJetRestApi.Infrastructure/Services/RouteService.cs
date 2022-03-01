using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Dtos.RouteService;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace JobJetRestApi.Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GeoRouteOptions _options;

        public RouteService(IHttpClientFactory httpClientFactory, 
            IOptions<GeoRouteOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<List<GeoPoint>> GetPointsBetweenTwoGeoPoints(GeoPoint firstGeoPoint, GeoPoint secondGeoPoint)
        {
            StringBuilder fullUriBuilder = new(_options.BaseUri);
            fullUriBuilder.Replace("{firstPointLongitude}", 
                firstGeoPoint.Longitude.ToString(new CultureInfo("en-US")));
            fullUriBuilder.Replace("{firstPointLatitude}", 
                firstGeoPoint.Latitude.ToString(new CultureInfo("en-US")));
            fullUriBuilder.Replace("{secondPointLongitude}", 
                secondGeoPoint.Longitude.ToString(new CultureInfo("en-US")));
            fullUriBuilder.Replace("{secondPointLatitude}", 
                secondGeoPoint.Latitude.ToString(new CultureInfo("en-US")));

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fullUriBuilder.ToString())
            {
                Headers =
                {
                    {HeaderNames.Accept, "application/json"}
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var decodedResponse = await JsonSerializer.DeserializeAsync<Root>(contentStream);

                if (decodedResponse is not null && decodedResponse.Code == "Ok" && decodedResponse.Routes.Any())
                {
                    var results = decodedResponse.Routes.First().Geometry.Coordinates;

                    var geoPoints = results
                        .Select(result => 
                            new GeoPoint(Convert.ToDecimal(result[0]), Convert.ToDecimal(result[1])))
                        .ToList();

                    return geoPoints;
                }
            }

            return new List<GeoPoint>();
        }
    }
}