using MoviesDatabase.Core.Gateways;

namespace MoviesDatabase.Infrastructure.Data.Prototyping
{
    public sealed class DataSeeder
    {
        private readonly IManager manager;

        public DataSeeder(IManager manager)
        {
            this.manager = manager;
        }

        public void Seed()
        {
        }
    }
}
