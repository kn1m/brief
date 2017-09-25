namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface INoteService
    {
        Task<BaseResponseMessage> CreateNote(NoteModel note);
    }
}