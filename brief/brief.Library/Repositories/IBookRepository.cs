namespace brief.Library.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IBookRepository
    {
        Task<Book> GetBook(Guid id);
        Task<bool> CheckBookForUniqueness(Book book);
        Task<Guid> CreateBook(Book book);
        Task<Guid> UpdateBook(Book book);
        Task RemoveBooks(IEnumerable<Book> books);
        Task RemoveBook(Book book);
    }
}