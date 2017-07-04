﻿namespace brief.Library.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Entities;

    public interface IAuthorRepository
    {
        Task AddAuthorToBook(Guid authorId, Guid bookId);
        Task<Author> GetAuthor(Guid id);
        Task<bool> CheckAuthorForUniqueness(Author author);
        Task<Guid> CreateAuthor(Author author);
        Task<Guid> UpdateAuthor(Author author);
        Task RemoveAuthor(Author author);
    }
}