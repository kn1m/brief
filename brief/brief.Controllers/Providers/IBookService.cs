namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IBookService
    {
        Task<ResponseMessage<(Guid authorId, Guid bookId)>> AddAuthorForBook(Guid authorId, Guid bookId);
        Task<ResponseMessage<(Guid authorId, Guid bookId)>> RemoveAuthorFromBook(Guid authorId, Guid bookId);
        Task<BaseResponseMessage> CreateBook(BookModel book, bool force = false);
        Task<BaseResponseMessage> UpdateBook(BookModel book);
        Task<BaseResponseMessage> RemoveBook(Guid id);
    }
}
