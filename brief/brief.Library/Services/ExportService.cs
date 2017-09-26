namespace brief.Library.Services
{
    using System.Threading.Tasks;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Repositories;
    using BaseServices;
    using Controllers.Models;
    using Entities;

    public class ExportService : BaseFileService, IExportService
    {
        public ExportService(INoteRepository noteRepository)
        {
            
        }

        public Task<BaseResponseMessage> ExportNotes(string filePath, NoteTypeModel noteType)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseMessage> ExportEdition(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public Note ExportNote(string noteText)
        {

            return null;
        }
    }
}
