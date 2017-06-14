namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IPublisherService
    {
        Task<BaseResponseMessage> CreatePublisher(PublisherModel publisher);
        Task<BaseResponseMessage> UpdatePublisher(PublisherModel publisher);
        Task<BaseResponseMessage> RemovePublisher(Guid id);
    }
}
