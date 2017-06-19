namespace brief.Data
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

        public Task<List<Edition>> GetEditionsByBook(Guid id)
            => Context.Set<Edition>().Where(e => e.Book.Id == id).ToListAsync();
        
        public Task<Edition> GetEdition(Guid id)
            => Context.Set<Edition>().FindAsync(id);
        
        public Task<Edition> CreateEdition(Edition edition)
        {
            throw new System.NotImplementedException();
        }

        public Task<Edition> UpdateEdition(Edition edition)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveEdition(Edition edition)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveEditions(IEnumerable<Edition> editions)
        {
            RemoveRange(editions);
            await Commit();
        }
    }
}
