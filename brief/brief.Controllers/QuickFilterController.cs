namespace brief.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Extensions;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using Models.RetrieveModels;

    public class QuickFilterController : ODataController
    {
        [HttpGet]
        [EnableQuery]
        [ODataRoute("book")]
        public PageResult<BookRetrieveModel> Get(ODataQueryOptions<BookRetrieveModel> options)
        {
            ODataQuerySettings settings = new ODataQuerySettings
            {
                PageSize = 5
            };

            IQueryable results = options.ApplyTo(new List<BookRetrieveModel> { new BookRetrieveModel() {Id = Guid.NewGuid(), Name = "Test" } }.AsQueryable(), settings);

            return new PageResult<BookRetrieveModel>(
                results as IEnumerable<BookRetrieveModel>,
                Request.ODataProperties().NextLink,
                Request.ODataProperties().TotalCount);
        }
    }
}
