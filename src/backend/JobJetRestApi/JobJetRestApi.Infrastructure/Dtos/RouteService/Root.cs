﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JobJetRestApi.Infrastructure.Dtos.RouteService
{
    public class Root
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("waypoints")]
        public List<Waypoint> Waypoints { get; set; }

        [JsonPropertyName("routes")]
        public List<Route> Routes { get; set; }
    }
}