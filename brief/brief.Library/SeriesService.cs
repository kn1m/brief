namespace brief.Library
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Providers;
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

        public Task<SeriesModel> CreateSeries(SeriesModel series)
        {
            throw new System.NotImplementedException();
        }

        public Task<SeriesModel> UpdateSeries(SeriesModel series)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveSeries(SeriesModel series)
        {
            throw new System.NotImplementedException();
        }
    }
}
