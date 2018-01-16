namespace brief.Data.Contexts.Interfaces
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        Task<int> Commit();
    }
}
