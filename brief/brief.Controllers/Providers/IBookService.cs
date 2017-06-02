namespace brief.Controllers.Providers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Models;
    using Models.RetrieveModels;

    public interface IBookService
    {
        Task<IQueryable<BookRetrieveModel>> GetBooks();
        Task<BookModel> CreateBook(BookModel book);
        Task<BookModel> UpdateBook(BookModel book);
        Task RemoveBook(BookModel book);
    }
}
