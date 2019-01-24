using System.Collections.Generic;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Core.Gateways;

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
    }
}
