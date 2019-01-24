using System;
using System.Collections.Generic;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Infrastructure.Data;

namespace MoviesDatabase.Infrastructure.Gateways
{
    public sealed class EntityFrameworkManager : IManager
    {
        private readonly EntityFrameworkDbContext context;
        private readonly IDictionary<Type, object> providers;

        public EntityFrameworkManager(EntityFrameworkDbContext context)
        {
            this.context = context;
            this.providers = new Dictionary<Type, object>();
        }

        public IProvider<T> GetProvider<T>() where T : Entity
        {
            var entityType = typeof(T);

            if (!this.providers.ContainsKey(entityType))
            {
                var providerType = typeof(EntityFrameworkProvider<T>);
                var providerInstance = Activator.CreateInstance(providerType, this.context);

                this.providers.Add(entityType, providerInstance);
            }

            return this.providers[entityType] as IProvider<T>;
        }

        public void SaveChanges() => this.context.SaveChanges();

        public void Dispose() => this.context.Dispose();
    }
}
