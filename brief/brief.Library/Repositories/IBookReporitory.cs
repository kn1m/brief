namespace brief.Library.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public interface IBookReporitory
    {
        IQueryable<Book> GetBooks();
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task RemoveBook(Guid id);
    }
}