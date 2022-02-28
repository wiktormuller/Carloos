using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
using JobJetRestApi.Infrastructure.Dtos.GeocodingService;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace JobJetRestApi.Infrastructure.Services
{
    public class GeocodingService : IGeocodingService
    { 
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly GeocodingOptions _options;

        public GeocodingService(IHttpClientFactory httpClientFactory, 
            IOptions<GeocodingOptions> options)
        {
            _httpClientFactory = Guard.Against.Null(httpClientFactory, nameof(httpClientFactory));
            _options = options.Value;
        }

        public async Task<AddressCoords> ConvertAddressIntoCoords(string address)
        {
            StringBuilder fullUriBuilder = new(_options.BaseUri);
            fullUriBuilder.Append(address);
            fullUriBuilder.Append($"&key={_options.ApiKey}");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fullUriBuilder.ToString())
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var decodedResponse = await JsonSerializer.DeserializeAsync<Root>(contentStream);

                if (decodedResponse is not null && decodedResponse.Status == "OK" && decodedResponse.Results.Any())
                {
                    var firstResult = decodedResponse.Results.First();
                    
                    return new AddressCoords(
                        address,
                        Convert.ToDecimal(firstResult.Geometry.Location.Lng),
                        Convert.ToDecimal(firstResult.Geometry.Location.Lat));
                }
            }
            
            return null;
        }
    }
}