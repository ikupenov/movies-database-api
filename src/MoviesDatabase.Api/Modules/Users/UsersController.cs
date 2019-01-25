using Microsoft.AspNetCore.Mvc;

namespace MoviesDatabase.Api.Modules.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser()
        {
            return Ok();
        }

        [HttpGet("{id}/rated-movies")]
        public IActionResult RatedMovies()
        {
            return Ok();
        }
    }
}