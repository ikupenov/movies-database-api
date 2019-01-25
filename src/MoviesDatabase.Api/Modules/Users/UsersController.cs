using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesDatabase.Core.Modules.Users;

namespace MoviesDatabase.Api.Modules.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserManager userManager;

        public UsersController(IMapper mapper, IUserManager userManager)
        {
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<UserDto>))]
        public IActionResult GetUsers()
        {
            var users = this.userManager.GetUsers();
            var usersDto = this.mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUser(Guid userId)
        {
            var user = this.userManager.GetUser(userId);

            if (user is null)
            {
                return NotFound();
            }

            var userDto = this.mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        //? can not reuse filters and sorts
        [HttpGet("{userId}/rated-movies")]
        public IActionResult RatedMovies(Guid userId)
        {
            return Ok();
        }
    }
}