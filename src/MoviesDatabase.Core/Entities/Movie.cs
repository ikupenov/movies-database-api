using System;
using System.Collections.Generic;

namespace MoviesDatabase.Core.Entities
{
    public class Movie : Entity
    {
        public string Title { get; set; }

        public double Rating { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ICollection<Genre> Genres { get; set; }
    }
}
