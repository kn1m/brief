namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
    using Controllers.Providers;
    using Repositories;

    public class AuthorService : IAuthorService
    {
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            
        }

        public async Task<BaseResponseMessage> CreateAuthor(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> UpdateAuthor(AuthorModel author)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> RemoveAuthor(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
