namespace brief.Controllers.Providers
{
    using System.Linq;
    using Models.RetrieveModels;

    public interface IDataService
    {
        IQueryable<BookRetrieveModel> GetBooks();
    }
}
