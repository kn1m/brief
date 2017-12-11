namespace brief.Library.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Repositories;
    using BaseServices;
    using Controllers.Models;
    using Helpers;

    public class ExportService : BaseFileService, IExportService
    {
        private readonly INoteRepository _noteRepository;

        public ExportService(IFileSystem fileSystem, INoteRepository noteRepository) : base(fileSystem)
        {
            Guard.AssertNotNull(noteRepository, nameof(noteRepository));

            _noteRepository = noteRepository;
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
