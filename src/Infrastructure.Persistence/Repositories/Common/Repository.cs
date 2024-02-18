using System.Linq.Expressions;
using Application.Common.Interfaces.Persistence;
using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Common;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext _dbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        Entities = _dbContext.Set<TEntity>();
    }

    private DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Query => Entities;
    public virtual IQueryable<TEntity> NoTrackingQuery => Entities.AsNoTracking();

    #region Get Query
    public IQueryable<TEntity> GetQuery(IInclude<TEntity>? include = null, bool noTracking = false)
    {
        return GetQuery(selector: x => x, include: include, noTracking: noTracking);
    }

    public IQueryable<TResult> GetQuery<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IInclude<TEntity>? include = null,
        bool noTracking = false
    )
    {
        var query = Entities.AsQueryable();

        if (noTracking)
        {
            query = Entities.AsNoTracking();
        }

        if (include != null)
            query = include.Execute(query);
        return query.Select(selector);
    }
    #endregion
    #region Add
    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        await Entities
            .AddAsync(entity: entity, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        _dbContext.Database.GetConnectionString();

        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        await Entities
            .AddRangeAsync(entities: entities, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    #endregion
    #region Delete
    public async Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.Remove(entity);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.RemoveRange(entities);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion
    #region Get
    public Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        IInclude<TEntity>? include = null,
        bool noTracking = false
    )
    {
        return GetAsync(
            predicate: predicate,
            selector: x => x,
            cancellationToken: cancellationToken,
            include: include,
            noTracking: noTracking
        );
    }

    public Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default,
        IInclude<TEntity>? include = null,
        bool noTracking = false
    )
    {
        var query = GetQuery(include: include, noTracking: noTracking);
        query = query.Where(predicate);
        return query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }
    #endregion
    #region Update
    public async Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.Update(entity);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default,
        bool saveNow = true
    )
    {
        Entities.UpdateRange(entities);
        if (saveNow)
            await _dbContext.SaveChangesAsync(cancellationToken);
    }
    #endregion
}
