namespace brief.Data
{
    using System;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class SeriesRepository : BaseRepository, ISeriesRepository
    {
        public SeriesRepository(IApplicationDbContext context) : base(context) { }

        public Task<bool> CheckSeriesForUniqueness(Series series)
        {
            throw new NotImplementedException();
        }

        public Task<Series> GetSeries(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateSerires(Series serires)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateSerires(Series serires)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSerires(Series serires)
        {
            throw new NotImplementedException();
        }
    }
}
