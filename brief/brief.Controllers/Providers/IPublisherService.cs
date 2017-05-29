namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models;

    public interface IPublisherService
    {
        Task<PublisherModel> CreatePublisher(PublisherModel publisher);
        Task<PublisherModel> UpdatePublisher(PublisherModel publisher);
        Task RemovePublisher(PublisherModel publisher);
    }
}
