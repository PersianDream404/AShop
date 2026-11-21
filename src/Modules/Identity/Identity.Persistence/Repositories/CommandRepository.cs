using Identity.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repositories;

public class CommandRepository<TEntity>(DbContext context) : ICommandRepository<TEntity>
    where TEntity : class
{
    protected DbSet<TEntity> Entities => context.Set<TEntity>();

    public virtual async Task<TEntity> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Entities.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Entities.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
}
