using Core;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class Context : DbContext, IRepository
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public IQueryable<T> EntitySet<T>() where T : class => Set<T>();

    public T Create<T>(T entity) where T : class => Set<T>().Add(entity).Entity;

    public void Delete<T>(T entity) where T : class
    {
        Set<T>().Remove(entity);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        new ContactConfiguration().Configure(modelBuilder.Entity<Contact>());
    }
}