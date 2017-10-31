namespace brief.Library.Services
{
    using System;
    using System.Threading.Tasks;
    using Controllers.Models;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Repositories;

    public class NoteService : INoteService
    {
        public NoteService(INoteRepository noteRepository)
        {
            
        }

        public Task<BaseResponseMessage> CreateNote(NoteModel note)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseMessage> UpdateNote(NoteModel note)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseMessage> RemoveNote(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
