namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface IBookRepository
    {
        Task<Book> GetBook(Guid id);
        Task<Guid> CreateBook(Book book);
        Task<Guid> UpdateBook(Book book);
        Task RemoveBook(Book book);
    }
}