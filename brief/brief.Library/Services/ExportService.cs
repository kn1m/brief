namespace brief.Library.Services
{
    using System.Threading.Tasks;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Repositories;

    class ExportService : IExportService
    {
        public ExportService(INoteRepository noteRepository)
        {
            
        }

        public Task<BaseResponseMessage> ExportNotes(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseMessage> ExportEdition(string filePath)
        {
            throw new System.NotImplementedException();
        }
    }
}
