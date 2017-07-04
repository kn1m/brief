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

    public class SeriesService : BaseImageService, ISeriesService
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
                             IMapper mapper)
        {
            Guard.AssertNotNull(seriesRepository);
            Guard.AssertNotNull(bookRepository);
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(coverRepository);
            Guard.AssertNotNull(mapper);

            _seriesRepository = seriesRepository;
            _editionRepository = editionRepository;
            _coverRepository = coverRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> AddBookToSeries(Guid bookId, Guid seriesId)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> CreateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var createdSeries = await _seriesRepository.CreateSerires(newSeries);

            return new BaseResponseMessage { Id = createdSeries };
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
                            covers.ForEach(c => CleanUp(c.LinkTo));

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
