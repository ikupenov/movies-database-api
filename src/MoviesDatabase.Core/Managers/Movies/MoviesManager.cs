using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Extensions;

namespace MoviesDatabase.Core.Managers.Movies
{
    public class MoviesManager : IMoviesManager
    {
        private readonly IManager manager;
        private readonly IProvider<Movie> moviesProvider;

        public MoviesManager(IManager manager)
        {
            this.manager = manager;
            this.moviesProvider = this.manager.GetProvider<Movie>();
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
    }
}
