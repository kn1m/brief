namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface ISeriesService
    {
        Task<BaseResponseMessage> AddBookToSeries(Guid bookId, Guid seriesId);
        Task<BaseResponseMessage> CreateSeries(SeriesModel series);
        Task<BaseResponseMessage> UpdateSeries(SeriesModel series);
        Task<BaseResponseMessage> RemoveSeries(Guid id, bool removeBooks = false);
    }
}
