using System;
using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Modules.Movies
{
    public interface IMovieManager
    {
        Movie GetMovie(Guid Id);

        IEnumerable<Movie> GetMovies();

        IEnumerable<Movie> GetMovies(MovieFilterModel searchModel);

        IEnumerable<Movie> GetMovies(MovieOrderModel orderModel);

        IEnumerable<Movie> GetMovies(MovieFilterModel searchModel, MovieOrderModel orderModel);

        void RateMovie(Movie movie, User user, float ratingValue);
    }
}
