namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IExportService
    {
        Task<BaseResponseMessage> ExportNotes(string filePath, NoteTypeModel noteType);
        Task<BaseResponseMessage> ExportEdition(string filePath);
    }
}