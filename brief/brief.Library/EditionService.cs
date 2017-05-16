namespace brief.Library
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;
    using Transformers;

    public class EditionService : BaseImageService, IEditionService
    {
        private readonly IEditionRepository _editionRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public EditionService(IEditionRepository editionRepository, ITransformer<string, string> transformer, IMapper mapper, StorageSettings settings) : base(settings)
        {
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);

            _editionRepository = editionRepository;
            _transformer = transformer;
            _mapper = mapper;
        }

        public async Task<EditionModel> CreateEdition(EditionModel edition)
        {
            var newEdtition = _mapper.Map<Edition>(edition);

            var createdEdition = await _editionRepository.CreateEdition(newEdtition);

            return _mapper.Map<EditionModel>(createdEdition);
        }

        public async Task<EditionModel> CreateEditionFromImage(ImageModel image)
        {
            var fileSavePath = SaveImage(image);

            if (fileSavePath == null)
            {
                return null;
            }

            var imagePath = ConvertToAppropirateFormat(fileSavePath, true);

            string transformResult = await _transformer.TransformAsync(imagePath);

            /// ... implelement parsing algo

            //Create new edititon
            //Create new book
            //Create new publisher
            //Create new series

            return null;
        }

        public async Task<EditionModel> UpdateEdition(EditionModel edition)
        {
            var updatedEdition = _mapper.Map<Edition>(edition);

            var createdEdition = await _editionRepository.UpdateEdition(updatedEdition);

            return _mapper.Map<EditionModel>(createdEdition);

        }

        public Task RemoveEdition(EditionModel edition)
        {
            throw new System.NotImplementedException();
        }
    }
}
