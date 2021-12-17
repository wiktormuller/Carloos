using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JobJetRestApi.Application.Interfaces;
using JobJetRestApi.Domain.Entities;
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
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
        }

        public async Task<AddressCoords> ConvertAddressIntoCoords(string address)
        {
            StringBuilder fullUri = new(_options.BaseUri);
            fullUri.Append(address);
            fullUri.Append($"&key={_options.ApiKey}");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fullUri.ToString())
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
    
    public class AddressComponent
    {
        [JsonPropertyName("long_name")]
        public string LongName { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class Northeast
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class Southwest
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }

    public class Viewport
    {
        [JsonPropertyName("northeast")]
        public Northeast Northeast { get; set; }

        [JsonPropertyName("southwest")]
        public Southwest Southwest { get; set; }
    }

    public class Geometry
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }

        [JsonPropertyName("viewport")]
        public Viewport Viewport { get; set; }
    }

    public class PlusCode
    {
        [JsonPropertyName("compound_code")]
        public string CompoundCode { get; set; }

        [JsonPropertyName("global_code")]
        public string GlobalCode { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }

        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("plus_code")]
        public PlusCode PlusCode { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}