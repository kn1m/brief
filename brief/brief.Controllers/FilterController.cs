namespace brief.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Extensions;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using Models.RetrieveModels;
    using Providers;

    public class FilterController : ODataController
    {
        private readonly IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("books({key})")]
        public SingleResult<BookRetrieveModel> Get([FromODataUri] Guid key)
        {
            IQueryable<BookRetrieveModel> result = _filterService.GetBookById(key);

            return SingleResult.Create(result);
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("books")]
        public PageResult<BookRetrieveModel> Get(ODataQueryOptions<BookRetrieveModel> options)
        {
            IQueryable results = options.ApplyTo(_filterService.GetBooks(), new ODataQuerySettings { PageSize = 5 });

            return new PageResult<BookRetrieveModel>(
                results as IEnumerable<BookRetrieveModel>,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount);
        }

        [HttpGet]
        [ODataRoute("covers({key})")]
        public HttpResponseMessage GetCover([FromODataUri] Guid key)
        {
            var cover = _filterService.GetCoverById(key);

            StreamContent sc = new StreamContent(new FileStream(cover.LinkTo, FileMode.Open));
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = sc;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
