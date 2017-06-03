namespace brief.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class BookRepository : BaseRepository, IBookReporitory
    {
        public BookRepository(IApplicationDbContext context) : base(context) {}

        public IQueryable<Book> GetBooks()
        {
            throw new NotImplementedException();
        }

        public async Task<Book> CreateBook(Book book)
        {
            var newBook = Add(book);
            await Commit();

            return newBook;
        }

        public Task<Book> UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBook(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
