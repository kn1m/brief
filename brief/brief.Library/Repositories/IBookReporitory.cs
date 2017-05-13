namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface IBookReporitory
    {
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task RemoveBook(Guid id);
    }
}