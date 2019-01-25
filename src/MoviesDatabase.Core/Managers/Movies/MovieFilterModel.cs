using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Managers.Movies
{
    public class MovieFilterModel
    {
        public string Title { get; set; }

        public int? YearOfRelease { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}
