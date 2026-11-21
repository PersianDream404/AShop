namespace Identity.Application.Contract.Interface;

public interface IQueryRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default);
}
