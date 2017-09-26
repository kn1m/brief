namespace brief.Data.Repositories
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

        public async Task<(Guid authorId, Guid bookId)> AddAuthorToBook(Guid authorId, Guid bookId)
        {
            await Connection.ExecuteAsync("insert into dbo.books_by_author (BookId, AuthorId)" +
                                          " values (@book, @author)",
                                          new
                                          {
                                              book = bookId,
                                              author = authorId
                                          });
            return (authorId, bookId);
        }

        public async Task<int> RemoveAuthorFromBook(Guid authorId, Guid bookId)
            => await Connection.ExecuteAsync("delete from dbo.books_by_author where BookId = @book and AuthorId = @author",
                new { book = bookId,
                      author = authorId });

        public async Task<bool> CheckAvailabilityAddingAuthorToBook(Guid authorId, Guid bookId)
        {
            var sql = "select count(*) from dbo.authors where Id = @author; " +
                      "select count(*) from dbo.books where Id = @book;" +
                      "select count(*) from dbo.books_by_author where AuthorId = @author and BookId = @book;";

            int authorsAmount;
            int booksAmount;
            int booksWithAuthors;

            using (SqlMapper.GridReader multi = await Connection.QueryMultipleAsync(sql, new { author = authorId, book = bookId }))
            {
                authorsAmount = multi.Read<int>().Single();
                booksAmount = multi.Read<int>().Single();
                booksWithAuthors = multi.Read<int>().Single();
            }

            if (booksAmount == 1 && authorsAmount == 1 && booksWithAuthors == 0)
            {
                return true;
            }

            return false;
        }

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
                                          " AuthorLastName = @authorLastName where Id = @id",
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
