namespace brief.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Models;
    using Providers;

    public class PublisherController : ApiController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        [HttpPost]
        public async Task<PublisherModel> Create([FromBody] PublisherModel publisher)
            => await _publisherService.CreatePublisher(publisher);

        [HttpPut]
        public async Task<PublisherModel> Update([FromBody] PublisherModel publisher)
            => await _publisherService.UpdatePublisher(publisher);

        [HttpDelete]
        public async Task Delete([FromUri] Guid id)
            => await _publisherService.RemovePublisher(id);
    }
}
