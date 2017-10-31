namespace brief.Library.Repositories
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface INoteRepository
    {
        List<Guid> AddMultipleNotes(IList<Note> notes);
    }
}
