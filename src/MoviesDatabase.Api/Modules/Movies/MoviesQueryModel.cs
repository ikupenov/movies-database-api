using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Core.Managers.Movies;

namespace MoviesDatabase.Api.Modules.Movies
{
    public class MoviesQueryModel
    {
        [FromQuery(Name = "title")]
        public string Title { get; set; }

        [Range(1800, 2500)]
        [FromQuery(Name = "year-of-release")]
        public int? YearOfRelease { get; set; }

        [FromQuery(Name = "genre")]
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

        [FromQuery(Name = "sort")]
        public SortProperty Sort { get; set; } = SortProperty.Title;

        [FromQuery(Name = "ascending")]
        public bool IsAscending { get; set; } = true;

        [FromQuery(Name = "skip")]
        public int Skip { get; set; } = 0;

        [FromQuery(Name = "take")]
        public int Take { get; set; } = int.MaxValue;
    }
}
