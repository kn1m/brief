namespace brief.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Extensions;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using Models;
    using Models.RetrieveModels;
    using Providers;

    public class FilterController : ODataController
    {
        private readonly IBookService _bookService;
        private readonly ODataQuerySettings _settings;

        public FilterController(IBookService bookService, ODataQuerySettings settings)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("book")]
        public async Task<PageResult<BookRetrieveModel>> Get(ODataQueryOptions<BookRetrieveModel> options)
        {
            //ODataQuerySettings settings = new ODataQuerySettings
            //{
            //    PageSize = 5
            //};

            //IQueryable results = options.ApplyTo(new List<BookRetrieveModel> { new BookRetrieveModel() {Id = Guid.NewGuid(), Name = "Test", Serieses = new List<SeriesModel>
            //{
            //    new SeriesModel() {Id = Guid.NewGuid(), Name = "tests111"}
            //}} }.AsQueryable(), _settings);

            IQueryable results = options.ApplyTo(await _bookService.GetBooks(), _settings);

            return new PageResult<BookRetrieveModel>(
                results as IEnumerable<BookRetrieveModel>,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount);
        }
    }
}
