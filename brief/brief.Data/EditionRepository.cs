namespace brief.Data
{
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Helpers;
    using Library.Repositories;
    public class EditionRepository : IEditionRepository
    {
        private readonly IApplicationDbContext _context;

        public EditionRepository(IApplicationDbContext context)
        {
            Guard.AssertNotNull(context);

            _context = context;
        }

        public Task<Edition> CreateEdition(Edition edition)
        {
            throw new System.NotImplementedException();
        }

        public Task<Edition> UpdateEdition(Edition edition)
        {
            throw new System.NotImplementedException();
        }

        public Task<Edition> RemoveEdition(Edition edition)
        {
            throw new System.NotImplementedException();
        }
    }
}
