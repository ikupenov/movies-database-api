using System;
using System.Collections.Generic;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Modules.Users
{
    public interface IUserManager
    {
        User GetUser(Guid id);

        IEnumerable<User> GetUsers();
    }
}
