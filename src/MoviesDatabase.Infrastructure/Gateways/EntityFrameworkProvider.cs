using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MoviesDatabase.Core.Gateways;
using MoviesDatabase.Core.Entities;
using MoviesDatabase.Infrastructure.Data;

namespace MoviesDatabase.Infrastructure.Gateways
{
    public class EntityFrameworkProvider<TEntity> : IProvider<TEntity>
        where TEntity : Entity
    {
        private readonly EntityFrameworkDbContext context;
        private readonly DbSet<TEntity> entities;

        public EntityFrameworkProvider(EntityFrameworkDbContext context)
        {
            this.context = context;
            this.entities = this.context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll() => this.entities;

        public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression) => this.entities.Where(expression);

        public void Create(TEntity entity) => this.entities.Add(entity);

        public void Update(TEntity entity) => this.entities.Update(entity);

        public void Delete(TEntity entity) => this.entities.Remove(entity);
    }
}
