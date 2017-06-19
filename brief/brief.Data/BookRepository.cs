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

        public async Task<Guid> CreateBook(Book book)
        {
            var newBook = Add(book);
            await Commit();

            return newBook.Id;
        }

        public async Task<Guid> UpdateBook(Book book)
        {
            Update(book);
            await Commit();

            return book.Id;
        }

        public async Task RemoveBook(Book book)
        {
            Remove(book);
            await Commit();
        }
    }
}
