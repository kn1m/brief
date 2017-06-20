namespace brief.Data
{
    using System;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    class PublisherRepostitory : BaseRepository, IPublisherRepository
    {
        public PublisherRepostitory(IApplicationDbContext appContext) : base(appContext) {}

        public Task<Publisher> GetPublisher(Guid id)
            => Context.Set<Publisher>().FindAsync(id);

        public Task<Guid> CreatePublisher(Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdatePublisher(Publisher publisher)
        {
            throw new NotImplementedException();
        }

        public Task RemovePublisher(Publisher publisher)
        {
            throw new NotImplementedException();
        }
    }
}
