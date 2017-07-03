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
            => await Connection.QueryFirstAsync<Author>("select Id, AuthorFirstName, AuthorSecondName, AuthorLastName " +
                                                                  "from dbo.authors where Id = @authorId", new { authorId = id });
        
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

        public async Task<Guid> CreateAuthor(Author author)
        {
            await Connection.ExecuteAsync("insert into dbo.authors (Id, AuthorFirstName, AuthorSecondName, AuthorLastName)" +
                                          " values (@id, @authorFirstName, @authorSecondName, @authorLastName)", 
                                          new
                                          {
                                              id = author.Id,
                                              authorFirstName = author.AuthorFirstName,
                                              authorSecondName = author.AuthorSecondName,
                                              authorLastName = author.AuthorLastName
                                          });
            return author.Id;
        }

        public async Task<Guid> UpdateAuthor(Author author)
        {
            await Connection.ExecuteAsync("update dbo.authors set AuthorFirstName = @authorFirstName," +
                                          " AuthorSecondName = @authorSecondName," +
                                          " AuthorLastName = @authorLastName) where Id = @id",
                new
                {
                    id = author.Id,
                    authorFirstName = author.AuthorFirstName,
                    authorSecondName = author.AuthorSecondName,
                    authorLastName = author.AuthorLastName
                });

            return author.Id;
        }

        public async Task RemoveAuthor(Author author)
            => await Connection.ExecuteAsync("delete from dbo.authors where Id = @authorId",
                new {authorId = author.Id});
    }
}
