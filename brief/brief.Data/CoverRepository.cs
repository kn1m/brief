namespace brief.Data
{
    using Library.Helpers;

    public class CoverRepository
    {
        private readonly IApplicationDbContext _context;

        public CoverRepository(IApplicationDbContext context)
        {
            Guard.AssertNotNull(context);

            _context = context;
        }
    }
}
