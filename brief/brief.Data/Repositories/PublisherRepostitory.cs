namespace brief.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using BaseRepositories;
    using Contexts.Interfaces;
    using Library.Entities;
    using Library.Repositories;

    class PublisherRepostitory : BaseRepository, IPublisherRepository
    {
        public PublisherRepostitory(IApplicationDbContext appContext) : base(appContext) {}

        public Task<bool> CheckPublisherForUniqueness(Publisher publisher)
            => Context.Set<Publisher>().AnyAsync(p => p.Name == publisher.Name && p.Founded == publisher.Founded);

        public Task<Publisher> GetPublisher(Guid id)
            => Context.Set<Publisher>().FindAsync(id);

        public async Task<Guid> CreatePublisher(Publisher publisher)
        {
            var newPublisher = Add(publisher);
            await Commit();

            return newPublisher.Id;
        }

        public async Task<Guid> UpdatePublisher(Publisher publisher)
        {
            Update(publisher);
            await Commit();

            return publisher.Id;
        }

        public async Task RemovePublisher(Publisher publisher)
        {
            Remove(publisher);
            await Commit();
        }
    }
}
