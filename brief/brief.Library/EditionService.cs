namespace brief.Library
{
    using System.IO;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Helpers;
    using Controllers.Models;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;
    using Transformers;

    public class EditionService : BaseImageService, IEditionService
    {
        public StorageSettings StorageSettings { get; }

        private readonly IEditionRepository _editionRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public EditionService(IEditionRepository editionRepository, ITransformer<string, string> transformer, IMapper mapper, BaseTransformerSettings settings, StorageSettings storageSettings) : base(settings)
        {
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(storageSettings);

            StorageSettings = storageSettings;

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
            var fileSavePath = Path.Combine(StorageSettings.StoragePath, image.Path);

            var imagePath = ConvertToAppropirateFormat(fileSavePath, true);

            string transformResult = await _transformer.TransformAsync(imagePath);

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
