namespace brief.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Helpers;

    public class BaseRepository
    {
        protected readonly IApplicationDbContext Context;

        protected BaseRepository(IApplicationDbContext appContext)
        {
            Guard.AssertNotNull(appContext);

            Context = appContext;
        }

        protected IQueryable<TEntity> RetrieveSet<TEntity>() where TEntity : class
            => Context.Set<TEntity>();

        protected TEntity Attach<TEntity>(TEntity entity) where TEntity : class 
            => Context.Set<TEntity>().Attach(entity);

        protected TEntity Add<TEntity>(TEntity entity) where TEntity : class
            => Context.Set<TEntity>().Add(entity);

        protected void Update<TEntity>(TEntity entity) where TEntity : class
            => Context.Entry(entity).State = EntityState.Modified;

        protected void Remove<TEntity>(TEntity entity) where TEntity : class
            => Context.Set<TEntity>().Remove(entity);

        protected void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
            => Context.Set<TEntity>().RemoveRange(entities);

        public Task Commit()
            => Context.Commit();
    }
}
