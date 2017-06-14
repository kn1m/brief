namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
    using Controllers.Providers;

    public class AuthorService : IAuthorService
    {
        public AuthorService()
        {
            
        }

        public Task<AuthorModel> CreateAuthor(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public Task<AuthorModel> UpdateAuthor(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAuthor(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> IAuthorService.CreateAuthor(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> IAuthorService.UpdateAuthor(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> IAuthorService.RemoveAuthor(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
