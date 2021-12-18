﻿using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.GeocodingService
{
    public class Northeast
    {
        [JsonPropertyName("lat")]
        public double Lat { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }
    }
}