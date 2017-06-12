namespace brief.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Models;
    using Providers;

    public class SeriesController : ApiController
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService ?? throw new ArgumentNullException(nameof(seriesService));
        }

        [HttpPost]
        public async Task<SeriesModel> Create([FromBody] SeriesModel series)
            => await _seriesService.CreateSeries(series);

        [HttpPut]
        public async Task<SeriesModel> Update([FromBody] SeriesModel series)
            => await _seriesService.CreateSeries(series);

        [HttpDelete]
        public async Task Delete([FromUri] Guid id)
            => await _seriesService.RemoveSeries(id);
    }
}
