namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface INoteService
    {
        Task<BaseResponseMessage> CreateNote(NoteModel note);
        Task<BaseResponseMessage> UpdateNote(NoteModel note);
        Task<BaseResponseMessage> RemoveNote(Guid id);
    }
}