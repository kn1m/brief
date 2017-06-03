namespace brief.Controllers
{
    using System;
    using System.Web.Http;
    using Providers;

    public class SeriesController : ApiController
    {
        private readonly ISeriesService _seriesService;

        public SeriesController(ISeriesService seriesService)
        {
            _seriesService = seriesService ?? throw new ArgumentNullException(nameof(seriesService));
        }
    }
}
