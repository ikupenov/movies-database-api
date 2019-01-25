﻿using System;
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

            var userRating = user.Ratings.FirstOrDefault(r => r.MovieId == movie.Id);

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
            else
            {
                var totalRatings = movie.Ratings.Count;
                var oldAverageRating = totalRatings > 1
                    ? ((movie.AverageRating * totalRatings) - userRating.Value) / (totalRatings - 1)
                    : 0;

                var oldAverageRatingRounded = Math.Round(oldAverageRating * 2) / 2;

                movie.AverageRating = Convert.ToSingle(oldAverageRatingRounded);

                userRating.Value = ratingValue;
            }

            var newAverageRating = movie.AverageRating + ((ratingValue - movie.AverageRating) / movie.Ratings.Count);
            var newAverageRatingRounded = Math.Round(newAverageRating * 2) / 2;

            movie.AverageRating = Convert.ToSingle(newAverageRatingRounded);

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

            return moviesQuery;
        }

        private IQueryable<Movie> SortMoviesQuery(IQueryable<Movie> moviesQuery, MovieOrderModel orderModel)
        {
            var orderProperty = TypeDescriptor.GetProperties(typeof(Movie)).Find(orderModel.Sort.GetName(), true);

            if (orderModel.IsAscending)
            {
                moviesQuery = moviesQuery.OrderBy(m => orderProperty.GetValue(m));
            }
            else
            {
                moviesQuery = moviesQuery.OrderByDescending(m => orderProperty.GetValue(m));
            }

            return moviesQuery.Skip(orderModel.Skip).Take(orderModel.Take);
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