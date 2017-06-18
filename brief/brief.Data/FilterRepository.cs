namespace brief.Data
{
    using System;
    using System.Linq;
    using Library.Entities;
    using Library.Repositories;
    using System.Data.Entity;

    public class FilterRepository : BaseRepository, IFilterRepository
    {
        public FilterRepository(IApplicationDbContext appContext) : base(appContext) {}

        public IQueryable<Book> GetBooks()
            => Context.Set<Book>();

        public IQueryable<Book> GetBookById(Guid id)
            => Context.Set<Book>().Where(b => b.Id == id)
                .Include(b => b.Serieses)
                .Include(b => b.Authors)
                .Include(b => b.Editions);
    }
}
