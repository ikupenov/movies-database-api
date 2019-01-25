using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesDatabase.Core.Modules.Movies;
using MoviesDatabase.Core.Modules.Users;

namespace MoviesDatabase.Api.Modules.Movies
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMovieManager movieManager;
        private readonly IUserManager userManager;

        public MoviesController(IMapper mapper, IMovieManager movieManager, IUserManager userManager)
        {
            this.mapper = mapper;
            this.movieManager = movieManager;
            this.userManager = userManager;
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

            var movies = this.movieManager.GetMovies(filterModel, orderModel);
            var moviesDto = this.mapper.Map<IEnumerable<MovieDto>>(movies);

            if (!movies.Any())
            {
                return NotFound();
            }

            return Ok(moviesDto);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MovieDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMovie(Guid movieId)
        {
            var movie = this.movieManager.GetMovie(movieId);

            if (movie is null)
            {
                return NotFound();
            }

            var movieDto = this.mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
        }

        [HttpPut("{movieId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(MovieDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult RateMovie(Guid movieId, [FromBody]RatingBodyModel ratingBody)
        {
            var movie = this.movieManager.GetMovie(movieId);

            if (movie is null)
            {
                return NotFound();
            }

            var user = this.userManager.GetUser(ratingBody.userId);

            if (user is null)
            {
                return NotFound();
            }

            this.movieManager.RateMovie(movie, user, ratingBody.rating);

            var movieDto = this.mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
        }
    }
}
