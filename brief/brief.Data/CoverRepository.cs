namespace brief.Data
{
    using Library.Helpers;
    using Library.Repositories;

    public class CoverRepository : ICoverRepository
    {
        private readonly IApplicationDbContext _context;

        public CoverRepository(IApplicationDbContext context)
        {
            Guard.AssertNotNull(context);

            _context = context;
        }
    }
}
