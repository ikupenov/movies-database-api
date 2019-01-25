using System;
using System.Collections.Generic;
using System.Linq;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Core.Gateways;

namespace MoviesDatabase.Core.Modules.Users
{
    public class UserManager : IUserManager
    {
        private readonly IManager manager;
        private readonly IProvider<User> userProvider;

        public UserManager(IManager manager)
        {
            this.manager = manager;
            this.userProvider = this.manager.GetProvider<User>();
        }

        public User GetUser(Guid id)
        {
            return this.userProvider.GetBy(u => u.Id == id).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return this.userProvider.GetAll();
        }
    }
}
