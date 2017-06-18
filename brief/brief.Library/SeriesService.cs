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

    public class SeriesService : ISeriesService
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly IMapper _mapper;

        public SeriesService(ISeriesRepository seriesRepository, IMapper mapper)
        {
            Guard.AssertNotNull(seriesRepository);
            Guard.AssertNotNull(mapper);

            _seriesRepository = seriesRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var createdSeries = await _seriesRepository.CreateSerires(newSeries);

            return new BaseResponseMessage { Id = createdSeries.Id };
        }

        public async Task<BaseResponseMessage> UpdateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var updatedSeries = await _seriesRepository.CreateSerires(newSeries);

            return new BaseResponseMessage {Id = updatedSeries.Id};
        }

        Task<BaseResponseMessage> ISeriesService.RemoveSeries(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
