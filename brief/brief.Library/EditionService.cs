namespace brief.Library
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Providers;
    using Helpers;
    using Repositories;
    using Transformers;

    public class EditionService : IEditionService
    {
        private readonly IEditionRepository _editionRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public EditionService(IEditionRepository editionRepository, ITransformer<string, string> transformer, IMapper mapper)
        {
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);

            _editionRepository = editionRepository;
            _transformer = transformer;
            _mapper = mapper;
        }

        public Task<EditionModel> CreateEdition(EditionModel edition)
        {
            throw new System.NotImplementedException();
        }

        public Task<EditionModel> CreateEditionFromImage(ImageModel image)
        {
            throw new System.NotImplementedException();
        }

        public Task<EditionModel> UpdateEdition(EditionModel edition)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveEdition(EditionModel edition)
        {
            throw new System.NotImplementedException();
        }
    }
}
