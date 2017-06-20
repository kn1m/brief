namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface IPublisherRepository
    {
        Task<Publisher> GetPublisher(Guid id);
        Task<Guid> CreatePublisher(Publisher publisher);
        Task<Guid> UpdatePublisher(Publisher publisher);
        Task RemovePublisher(Publisher publisher);
    }
}
