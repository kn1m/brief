namespace brief.Controllers.Providers
{
    using System;
    using System.Linq;
    using Models;
    using Models.RetrieveModels;

    public interface IFilterService
    {
        IQueryable<BookRetrieveModel> GetBooks();
        IQueryable<BookRetrieveModel> GetBookById(Guid id);
        CoverModel GetCoverById(Guid id);
    }
}
