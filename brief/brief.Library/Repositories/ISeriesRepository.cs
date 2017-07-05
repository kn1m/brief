namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface ISeriesRepository
    {
        Task<(Guid bookId, Guid seriesId)> AddBookToSeries(Guid bookId, Guid seriesId);
        Task<int> RemoveBookFromSeries(Guid bookId, Guid seriesId);
        Task<bool> CheckAvailabilityAddingBookToSeries(Guid bookId, Guid seriesId);
        Task<bool> CheckSeriesForUniqueness(Series series);
        Task<Series> GetSeries(Guid id);
        Task<Guid> CreateSerires(Series serires);
        Task<Guid> UpdateSerires(Series serires);
        Task RemoveSerires(Series serires);
    }
}
