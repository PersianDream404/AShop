using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Contract.Interface;

public class QueryRepository<T>(DbContext context) : IQueryRepository<T> where T : class
{
    protected DbSet<T> Entities => context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await Entities.FindAsync([id], cancellationToken);

    public virtual async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Entities.AsNoTracking().ToListAsync(cancellationToken);
}
