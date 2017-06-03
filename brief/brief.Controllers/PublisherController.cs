namespace brief.Controllers
{
    using System;
    using System.Web.Http;
    using Providers;

    public class PublisherController : ApiController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }
    }
}
