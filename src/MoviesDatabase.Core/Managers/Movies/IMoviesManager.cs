using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Managers.Movies
{
    public interface IMoviesManager
    {
        IEnumerable<Movie> GetMovies();

        IEnumerable<Movie> GetMovies(MovieFilterModel searchModel);

        IEnumerable<Movie> GetMovies(MovieOrderModel orderModel);

        IEnumerable<Movie> GetMovies(MovieFilterModel searchModel, MovieOrderModel orderModel);
    }
}
