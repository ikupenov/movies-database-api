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
            this.SeedUsers()
                .SeedMovies();
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
                Id = new Guid("5f7ccb53-bd89-49af-b5f8-dd6778d0342c"),
                Title = "Inception",
                Genres = new List<Genre> { Genre.Action, Genre.Drama },
                RunningTime = 148,
                ReleaseDate = new DateTime(2010, 06, 16),
                Rating = 8.8f
            };

            var movie2 = new Movie
            {
                Id = new Guid("29a0bdc3-0f99-4ff0-929a-e910fd1b2c0f"),
                Title = "Interstellar",
                Genres = new List<Genre> { Genre.Adventure, Genre.Drama, Genre.SciFi },
                RunningTime = 169,
                ReleaseDate = new DateTime(2014, 11, 07),
                Rating = 8.6f
            };

            var movie3 = new Movie
            {
                Id = new Guid("361c4549-7519-48ad-96fd-93292f44178b"),
                Title = "The Matrix",
                Genres = new List<Genre> { Genre.Action, Genre.SciFi },
                RunningTime = 136,
                ReleaseDate = new DateTime(1999, 03, 31),
                Rating = 8.7f
            };

            var movie4 = new Movie
            {
                Id = new Guid("8531515d-f2e5-47e4-a1f7-d9b6462d8494"),
                Title = "Forrest Gump",
                Genres = new List<Genre> { Genre.Drama, Genre.Romance },
                RunningTime = 142,
                ReleaseDate = new DateTime(1994, 07, 06),
                Rating = 8.8f
            };

            var movie5 = new Movie
            {
                Id = new Guid("5c8bee0a-facb-4ffa-8313-a09d9760ed0f"),
                Title = "The Shawshank Redemption",
                Genres = new List<Genre> { Genre.Drama },
                RunningTime = 142,
                ReleaseDate = new DateTime(1994, 10, 14),
                Rating = 9.3f
            };

            var movie6 = new Movie
            {
                Id = new Guid("72e445b1-75c9-4f2b-ae6e-46f8f1334960"),
                Title = "The Dark Knight",
                Genres = new List<Genre> { Genre.Action, Genre.Drama, Genre.Crime },
                RunningTime = 152,
                ReleaseDate = new DateTime(2008, 07, 18),
                Rating = 9.0f
            };

            moviesProvider.Create(movie1);
            moviesProvider.Create(movie2);
            moviesProvider.Create(movie3);
            moviesProvider.Create(movie4);
            moviesProvider.Create(movie5);
            moviesProvider.Create(movie6);
            this.manager.SaveChanges();

            return this;
        }
    }
}
