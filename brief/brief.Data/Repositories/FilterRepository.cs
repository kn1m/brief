namespace brief.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Library.Entities;
    using Library.Repositories;

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

        public Cover GetCoverById(Guid id)
            => Context.Set<Cover>().Find(id);
    }
}
