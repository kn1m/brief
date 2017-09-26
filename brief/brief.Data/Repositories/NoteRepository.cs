namespace brief.Data.Repositories
{
    using Library.Repositories;

    public class NoteRepository : BaseDapperRepository, INoteRepository
    {
        public NoteRepository(string connectionString) : base(connectionString) {}
    }
}
