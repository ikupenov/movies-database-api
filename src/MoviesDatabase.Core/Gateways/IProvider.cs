using System;
using System.Linq;
using System.Linq.Expressions;
using MoviesDatabase.Core.Models;

namespace MoviesDatabase.Core.Gateways
{
    public interface IProvider<TEntity>
        where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> expression);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
