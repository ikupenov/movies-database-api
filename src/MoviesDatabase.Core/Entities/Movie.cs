using System;
using System.Collections.Generic;

namespace MoviesDatabase.Core.Entities
{
    public class Movie : Entity
    {
        public string Title { get; set; }

        public float AverageRating { get; set; }

        public int RunningTime { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int YearOfRelease => ReleaseDate.Year;

        public ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
