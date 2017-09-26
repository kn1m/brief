namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models.BaseEntities;

    public interface IExportService
    {
        Task<BaseResponseMessage> ExportNotes(string filePath);
        Task<BaseResponseMessage> ExportEdition(string filePath);
    }
}