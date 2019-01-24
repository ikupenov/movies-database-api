using MoviesDatabase.Core.Gateways;

namespace MoviesDatabase.Core.Managers.Users
{
    public class UsersManager : IUsersManager
    {
        private readonly IManager manager;

        public UsersManager(IManager manager)
        {
            this.manager = manager;
        }
    }
}
