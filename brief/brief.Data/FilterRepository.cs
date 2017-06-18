namespace brief.Data
{
    using System;
    using System.Linq;
    using Library.Entities;
    using Library.Repositories;
    using System.Data.Entity;

    public class FilterRepository : BaseRepository, IFilterRepository
    {
        public FilterRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public IQueryable<Book> GetBooks()
        {
            return Context.Set<Book>()
                .Include(b => b.Serieses)
                .Include(b => b.Authors)
                .Include(b => b.Editions);
        }

        public IQueryable<Book> GetBookById(Guid id)
        {
            return Context.Set<Book>().Where(b => b.Id == id)
                .Include(b => b.Serieses)
                .Include(b => b.Authors)
                .Include(b => b.Editions);
        }
    }
}
