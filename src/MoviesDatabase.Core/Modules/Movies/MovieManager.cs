using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Extensions;

namespace MoviesDatabase.Core.Modules.Movies
{
    public class MovieManager : IMovieManager
    {
        private readonly IManager manager;
        private readonly IProvider<Movie> moviesProvider;

        public MovieManager(IManager manager)
        {
            this.manager = manager;
            this.moviesProvider = this.manager.GetProvider<Movie>();
        }

        public Movie GetMovie(Guid Id)
        {
            return this.moviesProvider.GetBy(m => m.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Movie> GetMovies()
        {
            return this.moviesProvider.GetAll();
        }

        public IEnumerable<Movie> GetMovies(MovieFilterModel filterModel)
        {
            var moviesQuery = this.moviesProvider.GetAll();

            this.FilterMoviesQuery(moviesQuery, filterModel);

            return moviesQuery;
        }

        public IEnumerable<Movie> GetMovies(MovieOrderModel orderModel)
        {
            var moviesQuery = this.moviesProvider.GetAll();

            this.SortMoviesQuery(moviesQuery, orderModel);

            return moviesQuery;
        }

        public IEnumerable<Movie> GetMovies(MovieFilterModel filterModel, MovieOrderModel orderModel)
        {
            var moviesQuery = this.moviesProvider.GetAll();

            moviesQuery = this.FilterMoviesQuery(moviesQuery, filterModel);
            moviesQuery = this.SortMoviesQuery(moviesQuery, orderModel);

            return moviesQuery;
        }

        public void RateMovie(Movie movie, User user, float ratingValue)
        {
            if (ratingValue < Rating.MinRating || Rating.MaxRating < ratingValue)
            {
                throw new ArgumentException($"Must be between {Rating.MinRating} and {Rating.MaxRating}", nameof(ratingValue));
            }

            GuardNull(movie, nameof(movie));
            GuardNull(user, nameof(user));

            var userRating = user.Ratings.FirstOrDefault(r => r.Movie.Id == movie.Id);

            if (userRating is null)
            {
                var rating = new Rating
                {
                    User = user,
                    Movie = movie,
                    Value = ratingValue
                };

                movie.Ratings.Add(rating);
                user.Ratings.Add(rating);
            }
            else if (userRating.Value == ratingValue)
            {
                return;
            }
            else
            {
                var oldAverageRating = SubstractValueFromAverage(movie.AverageRating, userRating.Value, movie.Ratings.Count);
                var oldAverageRatingRounded = RoundToNearestHalf(oldAverageRating);

                movie.AverageRating = oldAverageRatingRounded;

                userRating.Value = ratingValue;
            }

            var newAverageRating = AddValueToAverage(movie.AverageRating, ratingValue, movie.Ratings.Count);
            var newAverageRatingRounded = RoundToNearestHalf(newAverageRating);

            movie.AverageRating = newAverageRatingRounded;

            this.manager.SaveChanges();
        }

        private IQueryable<Movie> FilterMoviesQuery(IQueryable<Movie> moviesQuery, MovieFilterModel filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.Title))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(filterModel.Title, System.StringComparison.OrdinalIgnoreCase));
            }

            if (filterModel.YearOfRelease.HasValue)
            {
                moviesQuery = moviesQuery.Where(m => m.ReleaseDate.Year == filterModel.YearOfRelease.Value);
            }

            if (filterModel.Genres.Any())
            {
                moviesQuery = moviesQuery.Where(m => !filterModel.Genres.Except(m.Genres).Any());
            }

            if (filterModel.RatedByUser != null)
            {
                moviesQuery = moviesQuery.Where(m => m.Ratings.Any(r => r.User.Id == filterModel.RatedByUser.Id));
            }

            return moviesQuery;
        }

        private IQueryable<Movie> SortMoviesQuery(IQueryable<Movie> moviesQuery, MovieOrderModel orderModel)
        {
            var orderProperty = TypeDescriptor.GetProperties(typeof(Movie)).Find(orderModel.Sort.GetName(), true);

            if (orderModel.IsAscending)
            {
                moviesQuery = moviesQuery
                    .OrderBy(m => orderProperty.GetValue(m))
                    .ThenBy(m => m.Title);
            }
            else
            {
                moviesQuery = moviesQuery
                    .OrderByDescending(m => orderProperty.GetValue(m))
                    .ThenByDescending(m => m.Title);
            }

            return moviesQuery.Skip(orderModel.Skip).Take(orderModel.Take);
        }

        private static float AddValueToAverage(float average, float value, int totalCount)
        {
            var newAverage = average + ((value - average) / totalCount);

            return newAverage;
        }

        private static float SubstractValueFromAverage(float average, float value, int totalCount)
        {
            if (totalCount <= 1)
            {
                return 0;
            }

            var newAverage = ((average * totalCount) - value) / (totalCount - 1);

            return newAverage;
        }

        private static float RoundToNearestHalf(float number)
        {
            var roundedNumber = Math.Round(number * 2, MidpointRounding.AwayFromZero) / 2;

            return Convert.ToSingle(roundedNumber);
        }

        private static void GuardNull(object value, string paramName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
