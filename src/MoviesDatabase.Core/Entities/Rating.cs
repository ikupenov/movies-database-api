using System;

namespace MoviesDatabase.Core.Entities
{
    public class Rating
    {
        public const float MinRating = 1;
        public const float MaxRating = 5;

        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        public float Value { get; set; }

        public virtual User User { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
