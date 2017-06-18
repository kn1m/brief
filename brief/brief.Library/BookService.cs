namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
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

        public async Task<BookModel> UpdateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);

            var updatedBook = await _bookReporitory.UpdateBook(newBook);

            return _mapper.Map<BookModel>(updatedBook);
        }

        public async Task RemoveBook(Guid id)
        {
            await _bookReporitory.RemoveBook(id);
        }

        public async Task<BaseResponseMessage> CreateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);

            var createdBook = await _bookReporitory.CreateBook(newBook);

            return new BaseResponseMessage { Id = createdBook.Id };
        }

        Task<BaseResponseMessage> IBookService.UpdateBook(BookModel book)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> IBookService.RemoveBook(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
