using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public IActionResult GetMovies()
        {
            var movies = this.moviesManager.GetMovies();
            var moviesDto = this.mapper.Map<IEnumerable<MovieDto>>(movies);

            return Ok(moviesDto);
        }
    }
}
