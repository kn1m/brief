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
        private readonly ICoverRepository _coverRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public EditionService(IEditionRepository editionRepository,
                              ICoverRepository coverRepository,
                              ITransformer<string, string> transformer,
                              IMapper mapper,
                              BaseTransformerSettings settings,
                              StorageSettings storageSettings) : base(settings)
        {
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(coverRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(storageSettings);

            StorageSettings = storageSettings;

            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _transformer = transformer;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> RetrieveEditionDataFromImage(ImageModel image)
        {
            var imagePath = ConvertToAppropirateFormat(image.Path, deleteOriginal: true);

            string transformResult = await _transformer.TransformAsync(imagePath, image.TargetLanguage);

            CleanUp(imagePath);

            return new BaseResponseMessage { RawData = transformResult };
        }

        public async Task<BaseResponseMessage> UpdateEdition(EditionModel edition)
        {
            var updatedEdition = _mapper.Map<Edition>(edition);

            var createdEditionId = await _editionRepository.UpdateEdition(updatedEdition);

            return new BaseResponseMessage { Id = createdEditionId };
        }

        public async Task<BaseResponseMessage> RemoveEdition(Guid id)
        {
            var response = new BaseResponseMessage();

            var editionToRemove = await _editionRepository.GetEdition(id);

            if (editionToRemove == null)
            {
                response.RawData = $"Edition with {id} wasn't found.";

                return response;
            }

            await _editionRepository.RemoveEdition(editionToRemove);
            //remove covers
            response.Id = id;

            return response;
        }

        public async Task<BaseResponseMessage> CreateEdition(EditionModel edition)
        {
            var newEdtition = _mapper.Map<Edition>(edition);

            var response = new BaseResponseMessage();

            if (!await _editionRepository.CheckEditionForUniqueness(newEdtition))
            {
                response.RawData = $"Edition {newEdtition.Description} already existing with similar data.";
                return response;
            }

            var createdEditionId = await _editionRepository.CreateEdition(newEdtition);

            response.Id = createdEditionId;
            return response;
        }
    }
}
