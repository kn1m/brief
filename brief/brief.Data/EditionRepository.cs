namespace brief.Data
{
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
    }
}
