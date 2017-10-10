namespace brief.Controllers.Providers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IExportService
    {
        Task<BaseResponseMessage> ExportNotes(IList<NoteModel> notes, NoteTypeModel noteType);
        Task<BaseResponseMessage> SaveNotesFile(string notesFilePath, NoteTypeModel noteType);
    }
}