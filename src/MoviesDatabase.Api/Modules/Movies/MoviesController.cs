using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesDatabase.Core.Managers.Movies;

namespace MoviesDatabase.Api.Modules.Movies
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMoviesManager moviesManager;

        public MoviesController(IMapper mapper, IMoviesManager moviesManager)
        {
            this.mapper = mapper;
            this.moviesManager = moviesManager;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetMovies([FromQuery]MoviesQueryModel queryModel)
        {
            var filterModel = new MovieFilterModel
            {
                Title = queryModel.Title,
                YearOfRelease = queryModel.YearOfRelease,
                Genres = queryModel.Genres
            };

            var orderModel = new MovieOrderModel
            {
                Sort = queryModel.Sort,
                IsAscending = queryModel.IsAscending,
                Skip = queryModel.Skip,
                Take = queryModel.Take
            };

            var movies = this.moviesManager.GetMovies(filterModel, orderModel);
            var moviesDto = this.mapper.Map<IEnumerable<MovieDto>>(movies);

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(moviesDto);
        }
    }
}
