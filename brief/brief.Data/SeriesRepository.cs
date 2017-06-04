namespace brief.Data
{
    using System;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class SeriesRepository : BaseRepository, ISeriesRepository
    {
        private readonly IApplicationDbContext _context;

        public SeriesRepository(IApplicationDbContext context) : base(context) { }

        public Task<Series> CreateSerires(Series serires)
        {
            throw new NotImplementedException();
        }

        public Task<Series> UpdateSerires(Series serires)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSerires(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
