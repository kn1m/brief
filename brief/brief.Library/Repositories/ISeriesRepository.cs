namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface ISeriesRepository
    {
        Task<Series> CreateSerires(Series serires);
        Task<Series> UpdateSerires(Series serires);
        Task RemoveSerires(Guid id);
    }
}
