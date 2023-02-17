using System;
using System.Collections.Generic;
using System.Text;

namespace MovieMaster.Service.Model
{
    public enum AddMovieStatus
    {
        Added,
        Error
    }

    public class AddMovieResult
    {
        public AddMovieStatus Status { get; set; }
        public Movie Movie { get; set; }
    }
}
