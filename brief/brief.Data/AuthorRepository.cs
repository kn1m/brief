namespace brief.Data
{
    using System;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class AuthorRepository : BaseDapperRepository, IAuthorRepository
    {
        public Task<Author> GetAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckAuthorForUniqueness(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAuthor(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
