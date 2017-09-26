namespace brief.Library.Services
{
    using System.Threading.Tasks;
    using Controllers.Models;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;

    class NoteService : INoteService
    {
        public Task<BaseResponseMessage> CreateNote(NoteModel note)
        {
            throw new System.NotImplementedException();
        }
    }
}
