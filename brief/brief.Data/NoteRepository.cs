using brief.Library.Repositories;

namespace brief.Data
{
    public class NoteRepository : BaseDapperRepository, INoteRepository
    {
        public NoteRepository(string connectionString) : base(connectionString) {}
    }
}
