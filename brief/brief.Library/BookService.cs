namespace brief.Library
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Models.RetrieveModels;
    using Controllers.Providers;
    using Entities;
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

        public async Task<BookModel> CreateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);

            var createdBook = await _bookReporitory.CreateBook(newBook);

            return _mapper.Map<BookModel>(createdBook);
        }

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);

            var updatedBook = await _bookReporitory.UpdateBook(newBook);

            return _mapper.Map<BookModel>(updatedBook);
        }

        public Task RemoveBook(BookModel book)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BookRetrieveModel> GetBooks()
        {
            throw new NotImplementedException();
        }
    }
}
