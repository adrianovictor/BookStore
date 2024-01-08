using BookStore.Common.Extensions;
using BookStore.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.DataContexts.Common;

public abstract class BaseContext<TContext>(string connectionString) : DbContext
    where TContext: DbContext
{
    private const string DefaultConnectionString = "default";
    private readonly string ConnectionStringName = connectionString ?? DefaultConnectionString;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        OnCreating(modelBuilder);
    }

    protected virtual void OnCreating(ModelBuilder modelBuilder) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Save(async () =>
        {
            return await base.SaveChangesAsync(cancellationToken).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    // TODO: Adicionar log aqui
                }

                return task.Result;
            }, cancellationToken);
        });
    }

    protected virtual async Task<int> Save(Func<Task<int>> action)
    {
        int affectedRows;

        try
        {
            var entries = GetEntities();

            TraceAudit(entries);

            affectedRows = await action();

            await Task.CompletedTask;
        }
        catch (Exception)
        {
            // TODO: adicionar log aqui
            throw;
        }

        return affectedRows;
    }

    private IDictionary<object, EntityState> GetEntities()
    {
        return ChangeTracker.Entries()
                .Where(_ => _.State == EntityState.Added ||
                            _.State == EntityState.Modified ||
                            _.State == EntityState.Deleted ||
                            _.State == EntityState.Unchanged)
                .Select(_ => new { _.Entity, _.State })
                .DistinctBy(_ => _.Entity)
                .ToDictionary(_ => _.Entity, _ => _.State);
    }

    protected void TraceAudit(IDictionary<object, EntityState> entries)
    {
        entries.Where(_ => _.Value != EntityState.Unchanged).Each(entry =>
        {
            var auditEntity = entry.Key as IAudit;

            if (entry.Value == EntityState.Added && auditEntity != null)
            {
                auditEntity.CreatedAt = DateTimeOffset.Now.UtcDateTime;
            }

            if (entry.Value == EntityState.Modified && auditEntity != null)
            {
                auditEntity.UpdatedAt = DateTimeOffset.Now.UtcDateTime;
            }
        });
    }    
}
