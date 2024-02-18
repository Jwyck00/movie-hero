using System.Linq.Expressions;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces.Persistence;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Query { get; }
    IQueryable<TEntity> NoTrackingQuery { get; }
    
    // Add -----------------------------------------

    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );
    Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );

    // Delete -----------------------------------------

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveNow = true);
    Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );

    // Get -----------------------------------------

    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        IInclude<TEntity>? include = null
    );
    Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default,
        IInclude<TEntity>? include = null
    );

    // Get Query -----------------------------------------

    IQueryable<TEntity> GetQuery(IInclude<TEntity>? include = null);
    IQueryable<TResult> GetQuery<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IInclude<TEntity>? include = null
    );

    // Update -----------------------------------------

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default, bool saveNow = true);
    Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    );
}
