namespace brief.Data.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    class EditionFileRepository : IEditionFileRepository
    {
        public EditionFileRepository()
        {
            
        }

        public Task<Guid> AddFile(EditionFile file)
        {
            return null;
        }

        public Task<EditionFile> GetFile()
        {
            throw new NotImplementedException();
        }
    }
}
