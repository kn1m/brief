namespace brief.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Extensions;
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
        public async Task<HttpResponseMessage> AddBookToSeries([FromUri] Guid bookId, [FromUri] Guid seriesId)
        {
            var result = await _seriesService.AddBookToSeries(bookId, seriesId);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveBookFromSeries([FromUri] Guid bookId, [FromUri] Guid seriesId)
        {
            var result = await _seriesService.RemoveBookFromSeries(bookId, seriesId);

            return result.CreateRespose(Request, HttpStatusCode.Created, HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Create([FromBody] SeriesModel series)
        {
            var result = await _seriesService.CreateSeries(series);

            return result.CreateRespose(Request, HttpStatusCode.Created, HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update([FromBody] SeriesModel series)
        {
            var result = await _seriesService.UpdateSeries(series);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri] Guid id, [FromUri] bool removeBooks)
        {
            var result = await _seriesService.RemoveSeries(id, removeBooks);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }
    }
}
