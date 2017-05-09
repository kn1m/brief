namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models;

    public interface ISeriesService
    {
        Task<SeriesModel> CreateSeries(SeriesModel series);
        Task<SeriesModel> UpdateSeries(SeriesModel series);
        Task RemoveSeries(SeriesModel series);
    }
}
