namespace brief.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class EditionRepository : BaseRepository, IEditionRepository
    {
        public EditionRepository(IApplicationDbContext context) : base(context) {}

        public Task<bool> CheckEditionForUniqueness(Edition edition)
            => Context.Set<Edition>().AnyAsync(e => e.Isbn13 == edition.Isbn13);

        public Task<List<Edition>> GetEditionsByBookOrPublisher(Guid id)
            => Context.Set<Edition>().Where(e => e.BookId == id || e.PublisherId == id).ToListAsync();
        
        public Task<Edition> GetEdition(Guid id)
            => Context.Set<Edition>().FindAsync(id);
        
        public async Task<Guid> CreateEdition(Edition edition)
        {
            var newEdition = Add(edition);
            await Commit();

            return newEdition.Id;
        }

        public async Task<Guid> UpdateEdition(Edition edition)
        {
            Update(edition);
            await Commit();

            return edition.Id;
        }

        public async Task RemoveEdition(Edition edition)
        {
            Remove(edition);
            await Commit();
        }

        public async Task RemoveEditions(IEnumerable<Edition> editions)
        {
            RemoveRange(editions);
            await Commit();
        }
    }
}
