namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;

    public class AuthorService : IAuthorService
    {
        private readonly IEditionRepository _editionRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, 
                             IEditionRepository editionRepository,
                             ICoverRepository coverRepository,
                             IMapper mapper)
        {
            Guard.AssertNotNull(authorRepository);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(coverRepository);

            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreateAuthor(AuthorModel author)
        {
            var newAuthor = _mapper.Map<Author>(author);

            var response = new BaseResponseMessage();

            if (!await _authorRepository.CheckAuthorForUniqueness(newAuthor))
            {
                response.RawData = $"Author {author.AuthorFirstName} {author.AuthorSecondName} {author.AuthorLastName} already exists.";
                return response;
            }

            var createdAuthorId = await _authorRepository.CreateAuthor(newAuthor);

            response.Id = createdAuthorId;

            return response;
        }

        public async Task<BaseResponseMessage> UpdateAuthor(AuthorModel author)
        {
            var newAuthor = _mapper.Map<Author>(author);

            var response = new BaseResponseMessage();

            if (!await _authorRepository.CheckAuthorForUniqueness(newAuthor))
            {
                response.RawData = $"Author {author.AuthorFirstName} {author.AuthorSecondName} {author.AuthorLastName} already exists.";
                return response;
            }

            var updatedAuthorId = await _authorRepository.UpdateAuthor(newAuthor);

            response.Id = updatedAuthorId;

            return response;
        }

        public async Task<BaseResponseMessage> RemoveAuthor(Guid id, bool removeEditions = false)
        {
            var response = new BaseResponseMessage();

            var authorToRemove = await _authorRepository.GetAuthor(id);

            if (authorToRemove == null)
            {
                response.RawData = $"Author with {id} wasn't found.";
                return response;
            }

            var editionsToRemove = await _editionRepository.GetEditionsByBookOrPublisher(id);

            if (editionsToRemove != null)
            {
                editionsToRemove.ForEach(async e =>
                {
                    var covers = await _coverRepository.GetCoversByEdition(e.Id);

                    if (covers != null)
                    {
                        await _coverRepository.RemoveCovers(covers);
                    }
                });
                
                await _editionRepository.RemoveEditions(editionsToRemove);
            }

            await _authorRepository.RemoveAuthor(authorToRemove);

            response.Id = id;
            return response;
        }
    }
}
