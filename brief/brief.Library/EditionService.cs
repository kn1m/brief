namespace brief.Library
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Helpers;
    using Controllers.Models;
    using Controllers.Models.BaseEntities;
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

        public async Task<BaseResponseMessage> RetrieveEditionDataFromImage(ImageModel image)
        {
            var fileSavePath = Path.Combine(StorageSettings.StoragePath, image.Path);

            var imagePath = ConvertToAppropirateFormat(fileSavePath, deleteOriginal: true);

            string transformResult = await _transformer.TransformAsync(imagePath);

            CleanUp(imagePath);

            return new BaseResponseMessage { RawData = transformResult };
        }

        public Task<ResponseMessage<EditionModel>> RetrieveEditionObjectFromImage(ImageModel image)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> UpdateEdition(EditionModel edition)
        {
            var updatedEdition = _mapper.Map<Edition>(edition);

            var createdEdition = await _editionRepository.UpdateEdition(updatedEdition);

            return new BaseResponseMessage { Id = createdEdition.Id };
        }

        public async Task<BaseResponseMessage> RemoveEdition(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> CreateEdition(EditionModel edition)
        {
            var newEdtition = _mapper.Map<Edition>(edition);

            var createdEdition = await _editionRepository.CreateEdition(newEdtition);

            return new BaseResponseMessage { Id = createdEdition.Id };
        }

        public async Task<ResponseMessage<EditionModel>> GetByIsbnFromImage(ImageModel image)
        {
            var fileSavePath = Path.Combine(StorageSettings.StoragePath, image.Path);

            var imagePath = ConvertToAppropirateFormat(fileSavePath, deleteOriginal: true);

            string transformResult = await _transformer.TransformAsync(imagePath);

            CleanUp(imagePath);

            return new ResponseMessage<EditionModel>();
        }
    }
}
