namespace brief.Data
{
    using Library.Helpers;
    using Library.Repositories;

    public class SeriesRepository : ISeriesRepository
    {
        private readonly IApplicationDbContext _context;

        public SeriesRepository(IApplicationDbContext context)
        {
            Guard.AssertNotNull(context);

            _context = context;
        }
    }
}
