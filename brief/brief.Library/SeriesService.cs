namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using brief.Controllers.Models.BaseEntities;
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

        public async Task<SeriesModel> CreateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var createdSeries = await _seriesRepository.CreateSerires(newSeries);

            return _mapper.Map<SeriesModel>(createdSeries);
        }

        public async Task<SeriesModel> UpdateSeries(SeriesModel series)
        {
            var newSeries = _mapper.Map<Series>(series);

            var updatedSeries = await _seriesRepository.CreateSerires(newSeries);

            return _mapper.Map<SeriesModel>(updatedSeries);
        }

        public Task RemoveSeries(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> ISeriesService.CreateSeries(SeriesModel series)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> ISeriesService.UpdateSeries(SeriesModel series)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> ISeriesService.RemoveSeries(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
