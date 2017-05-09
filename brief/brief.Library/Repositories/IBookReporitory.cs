namespace brief.Library.Repositories
{
    using System.Threading.Tasks;
    using Entities;

    public interface IBookReporitory
    {
        Task<Book> CreateBook(Book book);
    }
}