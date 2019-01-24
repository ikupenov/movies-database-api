using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Managers.Movies
{
    public interface IMoviesManager
    {
        IEnumerable<Movie> GetMovies();
    }
}
