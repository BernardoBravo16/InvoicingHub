using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace InvoicingHub.Persistence
{
    public interface IDatabaseContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet<T> Set<T>() where T : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;
    }
}