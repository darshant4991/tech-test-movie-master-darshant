using System;
using System.Collections.Generic;

namespace MovieMaster.Service.Model
{
    public class Movie
    {
        public string Id { get; set; }
        public string MovieTitle { get; set; }
        public string Year { get; set; }
        public string Actors { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}