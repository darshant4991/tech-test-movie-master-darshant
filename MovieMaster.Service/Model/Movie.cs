using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MovieMaster.Service.Model
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; } //MovieTitle #Task1 Because it was trying to mapp "_mapper.Map<Movie>(movie)"
        public string Year { get; set; }
        public string Actors { get; set; }

        [JsonProperty("imdbRating")] //#Task4
        public string Rating { get; set; } //#Task3
        public DateTime LastUpdated { get; set; }
    }
}