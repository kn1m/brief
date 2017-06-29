namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IAuthorService
    {
        Task<BaseResponseMessage> CreateAuthor(AuthorModel author);
        Task<BaseResponseMessage> UpdateAuthor(AuthorModel author);
        Task<BaseResponseMessage> RemoveAuthor(Guid id, bool removeEditions = false);
    }
}