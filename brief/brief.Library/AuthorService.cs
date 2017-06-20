namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
    using Controllers.Providers;
    using Helpers;
    using Repositories;

    public class AuthorService : IAuthorService
    {
        private readonly IEditionRepository _editionRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, 
                             IEditionRepository editionRepository,
                             IMapper mapper)
        {
            Guard.AssertNotNull(authorRepository);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(editionRepository);

            _editionRepository = editionRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
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
