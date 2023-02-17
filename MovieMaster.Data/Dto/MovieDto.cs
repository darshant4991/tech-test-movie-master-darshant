using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieMaster.Data.Dto
{
    public class MovieDto
    {
        [JsonProperty("imdbId")]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Actors { get; set; }

        [JsonProperty("imdbRating")] //#Task4
        public string Rating { get; set; } //#Task3
        public DateTime LastUpdated { get; set; }
    }
}