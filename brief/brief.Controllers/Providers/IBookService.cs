namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IBookService
    {
        Task<BaseResponseMessage> AddAuthorForBook(Guid bookId, Guid seriesId);
        Task<BaseResponseMessage> CreateBook(BookModel book, bool force = false);
        Task<BaseResponseMessage> UpdateBook(BookModel book);
        Task<BaseResponseMessage> RemoveBook(Guid id);
    }
}
