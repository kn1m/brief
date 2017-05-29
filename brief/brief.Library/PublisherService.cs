namespace brief.Library
{
    using System.Threading.Tasks;
    using Controllers.Models;
    using Controllers.Providers;

    public class PublisherService : IPublisherService
    {
        public PublisherService()
        {
            
        }

        public Task<PublisherModel> CreatePublisher(PublisherModel publisher)
        {
            throw new System.NotImplementedException();
        }

        public Task<PublisherModel> UpdatePublisher(PublisherModel publisher)
        {
            throw new System.NotImplementedException();
        }

        public Task RemovePublisher(PublisherModel publisher)
        {
            throw new System.NotImplementedException();
        }
    }
}
