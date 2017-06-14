namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface ISeriesService
    {
        Task<BaseResponseMessage> CreateSeries(SeriesModel series);
        Task<BaseResponseMessage> UpdateSeries(SeriesModel series);
        Task<BaseResponseMessage> RemoveSeries(Guid id);
    }
}
