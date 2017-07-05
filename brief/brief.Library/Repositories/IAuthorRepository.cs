namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface IAuthorRepository
    {
        Task<(Guid authorId, Guid bookId)> AddAuthorToBook(Guid authorId, Guid bookId);
        Task<int> RemoveAuthorFromBook(Guid authorId, Guid bookId);
        Task<bool> CheckAvailabilityAddingAuthorToBook(Guid authorId, Guid bookId);
        Task<Author> GetAuthor(Guid id);
        Task<bool> CheckAuthorForUniqueness(Author author);
        Task<Guid> CreateAuthor(Author author);
        Task<Guid> UpdateAuthor(Author author);
        Task RemoveAuthor(Author author);
    }
}