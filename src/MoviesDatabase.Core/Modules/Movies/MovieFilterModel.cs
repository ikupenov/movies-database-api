using System;
using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Modules.Movies
{
    public class MovieFilterModel
    {
        public string Title { get; set; }

        public int? YearOfRelease { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public User RatedByUser { get; set; }
    }
}
