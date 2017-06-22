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
        private readonly IBookRepository _bookRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository,
                           IEditionRepository editionRepository,
                           IMapper mapper)
        {
            Guard.AssertNotNull(bookRepository);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(editionRepository);

            _editionRepository = editionRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreateBook(BookModel book, bool force = false)
        {
            var newBook = _mapper.Map<Book>(book);

            var createdBookId = await _bookRepository.CreateBook(newBook);

            return new BaseResponseMessage { Id = createdBookId };
        }

        public async Task<BaseResponseMessage> UpdateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);

            var response = new BaseResponseMessage();

            var bookToUpdate = await _bookRepository.GetBook(newBook.Id);

            if (!await _bookRepository.CheckBookForUniqueness(newBook))
            {
                response.RawData = $"Book {newBook.Name} already existing with similar data.";

                return response;
            }

            if (bookToUpdate == null)
            {
                response.RawData = $"Book with {newBook.Id} wasn't found.";

                return response;
            }

            await _bookRepository.UpdateBook(newBook);

            response.Id = newBook.Id;

            return response;
        }

        public async Task<BaseResponseMessage> RemoveBook(Guid id)
        {
            var response = new BaseResponseMessage();

            var bookToRemove = await _bookRepository.GetBook(id);

            if (bookToRemove == null)
            {
                response.RawData = $"Book with {id} wasn't found.";

                return response;
            }

            var editionsToRemove = await _editionRepository.GetEditionsByBookOrPublisher(id);

            await _editionRepository.RemoveEditions(editionsToRemove);

            await _bookRepository.RemoveBook(bookToRemove);

            response.Id = id;

            return response;
        }
    }
}
