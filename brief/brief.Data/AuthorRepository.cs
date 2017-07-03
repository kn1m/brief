namespace brief.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Library.Entities;
    using Library.Repositories;

    public class AuthorRepository : BaseDapperRepository, IAuthorRepository
    {
        public AuthorRepository(string connectionString) : base(connectionString) {}

        public async Task<Author> GetAuthor(Guid id)
        {
            var author = await Connection.QueryFirstAsync<Author>("select Id, AuthorFirstName, AuthorSecondName, AuthorLastName " +
                                                                  "from dbo.authors where Id = @authorId", new { authorId = id });

            return author;
        }

        public async Task<bool> CheckAuthorForUniqueness(Author author)
        {
            var existingCount = (await Connection.QueryAsync<int>("select count(*) from dbo.authors where AuthorFirstName = @authorFirstName and " +
                                                                  "AuthorSecondName = @authorSecondName and AuthorLastName = @authorLastName",
                                                                  new
                                                                  {
                                                                      authorFirstName = author.AuthorFirstName,
                                                                      authorSecondName = author.AuthorSecondName,
                                                                      authorLastName = author.AuthorLastName
                                                                  })).Single();

            if (existingCount != 0)
            {
                return false;
            }
            return true;
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
