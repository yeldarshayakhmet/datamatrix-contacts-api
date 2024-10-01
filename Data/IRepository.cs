namespace Data;

public interface IRepository
{
    // Abstract data layer communication
    // Usually a best practice for decoupling from a specific database provider
    IQueryable<T> EntitySet<T>() where T : class;
    T Create<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task SaveAsync(CancellationToken cancellationToken = default);
}