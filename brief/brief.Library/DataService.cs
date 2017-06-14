namespace brief.Library
{
    using System;
    using System.Linq;
    using Controllers.Models.RetrieveModels;
    using Controllers.Providers;

    public class DataService : IDataService
    {
        public DataService()
        {
            
        }

        public IQueryable<BookRetrieveModel> GetBooks()
        {
            throw new NotImplementedException();
        }
    }
}
