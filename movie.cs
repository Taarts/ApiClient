using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace ApiClient
{
    public class movies
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("rt_score")]
        public string RottenTomatoScore { get; set; }
    }
}