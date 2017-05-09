namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Providers;
    using Helpers;
    using Repositories;

    public class BookService : IBookService
    {
        private readonly IBookReporitory _bookReporitory;
        private readonly IMapper _mapper;

        public BookService(IBookReporitory bookReporitory, IMapper mapper)
        {
            Guard.AssertNotNull(bookReporitory);
            Guard.AssertNotNull(mapper);

            _bookReporitory = bookReporitory;
            _mapper = mapper;
        }

        public Task<BookModel> CreateBook(BookModel book)
        {
            throw new NotImplementedException();
        }

        public Task<BookModel> UpdateBook(BookModel book)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBook(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
