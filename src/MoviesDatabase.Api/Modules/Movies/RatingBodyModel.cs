using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesDatabase.Api.Modules.Movies
{
    public class RatingBodyModel
    {
        [Required]
        public Guid userId;

        [Required]
        [Range(1, 5)]
        public float rating;
    }
}
