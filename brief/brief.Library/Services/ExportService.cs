namespace brief.Library.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Repositories;
    using BaseServices;
    using Controllers.Models;

    public class ExportService : BaseFileService, IExportService
    {
        public ExportService(INoteRepository noteRepository)
        {
            
        }

        public Task<BaseResponseMessage> ExportNotes(IList<NoteModel> notes, NoteTypeModel noteType)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResponseMessage> SaveNotesFile(string notesFilePath, NoteTypeModel noteType)
        {
            throw new System.NotImplementedException();
        }
    }
}
