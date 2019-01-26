using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesDatabase.Api.Modules.Movies
{
    public class RatingBodyModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public float Rating { get; set; }
    }
}
