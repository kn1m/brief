namespace brief.Library
{
    using System.Linq;
    using AutoMapper;
    using Controllers.Models.RetrieveModels;
    using Controllers.Providers;
    using Helpers;
    using Repositories;

    public class DataService : IDataService
    {
        private readonly IBookReporitory _bookReporitory;
        private readonly IMapper _mapper;

        public DataService(IBookReporitory bookReporitory, IMapper mapper)
        {
            Guard.AssertNotNull(_bookReporitory);
            Guard.AssertNotNull(mapper);

            _bookReporitory = bookReporitory;
            _mapper = mapper;
        }

        public IQueryable<BookRetrieveModel> GetBooks()
        {
            return null;
        }
    }
}
