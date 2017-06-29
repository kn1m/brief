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
        private readonly ICoverRepository _coverRepository;
        private readonly IMapper _mapper;
        
        public BookService(IBookRepository bookRepository,
                           IEditionRepository editionRepository,
                           ICoverRepository coverRepository,
                           IMapper mapper)
        {
            Guard.AssertNotNull(bookRepository);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(coverRepository);

            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreateBook(BookModel book, bool force = false)
        {
            var newBook = _mapper.Map<Book>(book);

            var response = new BaseResponseMessage();

            if (!await _bookRepository.CheckBookForUniqueness(newBook) || !force)
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
