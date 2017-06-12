namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IAuthorService
    {
        Task<AuthorModel> CreateAuthor(AuthorModel author);
        Task<AuthorModel> UpdateAuthor(AuthorModel author);
        Task RemoveAuthor(Guid id);
    }
}