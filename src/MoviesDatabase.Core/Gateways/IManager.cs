using System;
using MoviesDatabase.Core.Entities;

namespace MoviesDatabase.Core.Gateways
{
    public interface IManager : IDisposable
    {
        IProvider<T> GetProvider<T>() where T : Entity;

        void SaveChanges();
    }
}
