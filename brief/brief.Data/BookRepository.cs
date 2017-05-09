namespace brief.Data
{
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class BookRepository : BaseRepository, IBookReporitory
    {
        public BookRepository(IApplicationDbContext context) : base(context) {}

        public async Task<Book> CreateBook(Book book)
        {
            var newBook = Add(book);
            await Commit();

            return newBook;
        }
    }
}
