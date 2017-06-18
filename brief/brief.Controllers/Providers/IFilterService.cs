namespace brief.Controllers.Providers
{
    using System;
    using System.Linq;
    using Models.RetrieveModels;

    public interface IFilterService
    {
        IQueryable<BookRetrieveModel> GetBooks();
        IQueryable<BookRetrieveModel> GetBookById(Guid id);
    }
}
