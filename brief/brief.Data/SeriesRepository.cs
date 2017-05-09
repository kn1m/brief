namespace brief.Data
{
    using Library.Helpers;

    public class SeriesRepository
    {
        private readonly IApplicationDbContext _context;

        public SeriesRepository(IApplicationDbContext context)
        {
            Guard.AssertNotNull(context);

            _context = context;
        }
    }
}
