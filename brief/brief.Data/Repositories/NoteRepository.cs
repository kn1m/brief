namespace brief.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using Library.Entities;
    using Library.Repositories;

    public class NoteRepository : BaseDapperRepository, INoteRepository
    {
        public NoteRepository(string connectionString) : base(connectionString) {}

        public List<Guid> AddMultipleNotes(IList<Note> notes)
        {
            throw new NotImplementedException();
        }
    }
}
