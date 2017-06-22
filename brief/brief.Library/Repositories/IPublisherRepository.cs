namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface IPublisherRepository
    {
        Task<bool> CheckPublisherForUniqueness(Publisher publisher);
        Task<Publisher> GetPublisher(Guid id);
        Task<Guid> CreatePublisher(Publisher publisher);
        Task<Guid> UpdatePublisher(Publisher publisher);
        Task RemovePublisher(Publisher publisher);
    }
}
