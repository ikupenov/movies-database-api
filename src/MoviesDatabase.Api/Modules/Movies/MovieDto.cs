using System;
using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Api.Modules.Movies
{
    public class MovieDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public float AverageRating { get; set; }

        public int RunningTime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}
