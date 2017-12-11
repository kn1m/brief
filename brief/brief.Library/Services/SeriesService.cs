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

    public class SeriesService : BaseFileService, ISeriesService
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMapper _mapper;

        public SeriesService(ISeriesRepository seriesRepository, 
                             IBookRepository bookRepository,
                             IEditionRepository editionRepository,
                             ICoverRepository coverRepository,
                             IFileSystem fileSystem,
                             IMapper mapper) : base(fileSystem)
        {
            Guard.AssertNotNull(seriesRepository, nameof(seriesRepository));
            Guard.AssertNotNull(bookRepository, nameof(bookRepository));
            Guard.AssertNotNull(editionRepository, nameof(editionRepository));
            Guard.AssertNotNull(coverRepository, nameof(coverRepository));
            Guard.AssertNotNull(mapper, nameof(mapper));

            _seriesRepository = seriesRepository;
            _editionRepository = editionRepository;
            _coverRepository = coverRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ResponseMessage<(Guid bookId, Guid seriesId)>> AddBookToSeries(Guid bookId, Guid seriesId)
        {
            var response = new ResponseMessage<(Guid bookId, Guid seriesId)>();

            if (!await _seriesRepository.CheckAvailabilityAddingBookToSeries(bookId, seriesId))
            {
                response.RawData = $"Can't add book with id {bookId} to series with id {seriesId}.";
                return response;
            }

            response.Payload = await _seriesRepository.AddBookToSeries(bookId, seriesId);
            return response;
        }

        public async Task<ResponseMessage<(Guid bookId, Guid seriesId)>> RemoveBookFromSeries(Guid bookId, Guid seriesId)
        {
            var response = new ResponseMessage<(Guid bookId, Guid seriesId)>();

            if (await _seriesRepository.RemoveBookFromSeries(bookId, seriesId) == 0)
            {
                response.RawData = $"Linked record with series id {seriesId} and book id {bookId} wasn't found.";
                return response;
            }

            response.Payload = (bookId, seriesId);
            return response;
        }

        public async Task<BaseResponseMessage> CreateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var response = new BaseResponseMessage();

            if (!await _seriesRepository.CheckSeriesForUniqueness(newSeries))
            {
                response.RawData = $"Series {newSeries.Name} already existing with similar data.";
                return response;
            }

            var createdSeriesId = await _seriesRepository.CreateSerires(newSeries);

            response.Id = createdSeriesId;
            return response;
        }

        public async Task<BaseResponseMessage> UpdateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var response = new BaseResponseMessage();

            var seriesToUpdate = await _seriesRepository.GetSeries(newSeries.Id);

            if (seriesToUpdate == null)
            {
                response.RawData = $"Series with {newSeries.Id} wasn't found.";
                return response;
            }
            
            if (seriesToUpdate.Equals(newSeries))
            {
                response.RawData = $"Series {newSeries.Name} already existing with similar data.";
                return response;
            }

            await _seriesRepository.UpdateSerires(seriesToUpdate);

            response.Id = newSeries.Id;
            return response;
        }

        public async Task<BaseResponseMessage> RemoveSeries(Guid id, bool removeBooks)
        {
            var response = new BaseResponseMessage();

            var seriesToRemove = await _seriesRepository.GetSeries(id);

            if (seriesToRemove == null)
            {
                response.RawData = $"Series with {id} wasn't found.";
                return response;
            }

            if (removeBooks)
            {
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

                if (seriesToRemove.BooksInSeries != null)
                {
                    await _bookRepository.RemoveBooks(seriesToRemove.BooksInSeries);
                }
            }
            
            await _seriesRepository.RemoveSerires(seriesToRemove);

            response.Id = id;
            return response;
        }
    }
}
