using System;
using System.Collections.Generic;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Core.Gateways;

namespace MoviesDatabase.Infrastructure.Data.Prototyping
{
    public sealed class DataSeeder
    {
        private readonly IManager manager;

        public DataSeeder(IManager manager)
        {
            this.manager = manager;
        }

        public void Seed()
        {
            this.SeedUsers();
        }

        private DataSeeder SeedUsers()
        {
            var usersProvider = this.manager.GetProvider<User>();

            var user1 = new User
            {
                Id = new Guid("0ef27a5d-b9f2-4470-96d2-7123fab03460"),
                Username = "john.doe"
            };

            usersProvider.Create(user1);
            this.manager.SaveChanges();

            return this;
        }

        private DataSeeder SeedMovies()
        {
            var moviesProvider = this.manager.GetProvider<Movie>();

            var movie1 = new Movie
            {
                Title = "Inception",
                Genres = new List<Genre> { Genre.Action, Genre.Drama },
                ReleaseDate = new DateTime(2010, 06, 16),
                Rating = 8.8
            };

            moviesProvider.Create(movie1);
            this.manager.SaveChanges();

            return this;
        }
    }
}
