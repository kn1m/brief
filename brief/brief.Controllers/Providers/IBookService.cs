namespace brief.Controllers.Providers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Models.RetrieveModels;

    public interface IBookService
    {
        IQueryable<BookRetrieveModel> GetBooks();
        Task<BookModel> CreateBook(BookModel book);
        Task<BookModel> UpdateBook(BookModel book);
        Task RemoveBook(Guid id);
    }
}
