namespace brief.Data
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using Library.Helpers;

    public class BaseRepository
    {
        protected readonly IApplicationDbContext context;

        protected BaseRepository(IApplicationDbContext appContext)
        {
            Guard.AssertNotNull(context);

            context = appContext;
        }

        protected TEntity Attach<TEntity>(TEntity entity) where TEntity : class 
            => context.Set<TEntity>().Attach(entity);

        protected TEntity Add<TEntity>(TEntity entity) where TEntity : class
            => context.Set<TEntity>().Add(entity);

        protected void Update<TEntity>(TEntity entity) where TEntity : class
            => context.Entry(entity).State = EntityState.Modified;

        protected void Remove<TEntity>(TEntity entity) where TEntity : class
            => context.Set<TEntity>().Remove(entity);

        public Task Commit()
            => context.Commit();
    }
}
