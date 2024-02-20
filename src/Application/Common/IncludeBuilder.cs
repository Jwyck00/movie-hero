using Application.Common.Interfaces.Persistence;
using Domain.Common.Interfaces;

namespace Application.Common
{
    public class IncludeBuilder<TEntity> : IInclude<TEntity> where TEntity : class, IEntity
    {
        private readonly Func<IQueryable<TEntity>, IQueryable<TEntity>> _include;

        public IncludeBuilder(Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
        {
            _include = include;
        }

        public T Execute<T>(T query) where T : IQueryable<TEntity>
        {
            return (T)_include.Invoke(query);
        }
    }
}
