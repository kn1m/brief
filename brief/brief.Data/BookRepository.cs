namespace brief.Data
{
    using System;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base(context) {}

        public Task<Book> GetBook(Guid id)
            => Context.Set<Book>().FindAsync(id);

        public async Task<Book> CreateBook(Book book)
        {
            var newBook = Add(book);
            await Commit();

            return newBook;
        }

        public async Task<Book> UpdateBook(Book book)
        {
            Update(book);
            await Commit();

            return book;
        }

        public async Task RemoveBook(Book book)
        {
            Remove(book);
            await Commit();
        }
    }
}
