namespace brief.Library.Services
{
    using System;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using AutoMapper;
    using BaseServices;
    using Controllers.Models;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;

    public class BookService : BaseFileService, IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        
        public BookService(IBookRepository bookRepository,
                           IEditionRepository editionRepository,
                           ICoverRepository coverRepository,
                           IAuthorRepository authorRepository,
                           IFileSystem fileSystem,
                           IMapper mapper) : base(fileSystem)
        {
            Guard.AssertNotNull(bookRepository, nameof(bookRepository));
            Guard.AssertNotNull(mapper, nameof(mapper));
            Guard.AssertNotNull(editionRepository, nameof(editionRepository));
            Guard.AssertNotNull(authorRepository, nameof(authorRepository));
            Guard.AssertNotNull(coverRepository, nameof(coverRepository));

            _authorRepository = authorRepository;
            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ResponseMessage<(Guid authorId, Guid bookId)>> AddAuthorForBook(Guid authorId, Guid bookId)
        {
            var response = new ResponseMessage<(Guid authorId, Guid bookId)>();

            if (!await _authorRepository.CheckAvailabilityAddingAuthorToBook(authorId, bookId))
            {
                response.RawData = $"Can't add author with id {authorId} to book with id {bookId}";
                return response;
            }

            response.Payload = await _authorRepository.AddAuthorToBook(authorId, bookId);
            return response;
        }

        public async Task<ResponseMessage<(Guid authorId, Guid bookId)>> RemoveAuthorFromBook(Guid authorId, Guid bookId)
        {
            var response = new ResponseMessage<(Guid authorId, Guid bookId)>();

            if (await _authorRepository.RemoveAuthorFromBook(authorId, bookId) == 0)
            {
                response.RawData = $"Linked record with author id {authorId} and book with id {bookId} wasn't found.";
                return response;
            }

            response.Payload = (authorId, bookId);
            return response;
        }

        public async Task<BaseResponseMessage> CreateBook(BookModel book, bool force = false)
        {
            var newBook = _mapper.Map<Book>(book);

            var response = new BaseResponseMessage();

            if (await _bookRepository.CheckBookForUniqueness(newBook) && !force)
            {
                response.RawData = $"Book {newBook.Name} already existing with similar data.";
                return response;
            }

            var createdBookId = await _bookRepository.CreateBook(newBook);

            response.Id = createdBookId;
            return response;
        }

        public async Task<BaseResponseMessage> UpdateBook(BookModel book)
        {
            var newBook = _mapper.Map<Book>(book);

            var response = new BaseResponseMessage();

            var bookToUpdate = await _bookRepository.GetBook(newBook.Id);

            if (bookToUpdate == null)
            {
                response.RawData = $"Book with {newBook.Id} wasn't found.";
                return response;
            }
            
            if (bookToUpdate.Equals(newBook))
            {
                response.RawData = $"Book {newBook.Name} already existing with similar data.";
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

            if (editionsToRemove != null)
            {
                editionsToRemove.ForEach(async e =>
                {
                    var covers = await _coverRepository.GetCoversByEdition(e.Id);

                    if (covers != null)
                    {
                        covers.ForEach(c => TryCleanUp(c.LinkTo));

                        await _coverRepository.RemoveCovers(covers);
                    }
                });
                
                await _editionRepository.RemoveEditions(editionsToRemove);
            }

            await _bookRepository.RemoveBook(bookToRemove);

            response.Id = id;
            return response;
        }
    }
}
