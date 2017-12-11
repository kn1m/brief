namespace brief.Library.Services
{
    using System;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using AutoMapper;
    using BaseServices;
    using Controllers.Helpers;
    using Controllers.Models;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;
    using Transformers;

    public class EditionService : BaseTransformService, IEditionService
    {
        public StorageSettings StorageSettings { get; }

        private readonly IEditionRepository _editionRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public EditionService(IEditionRepository editionRepository,
                              ICoverRepository coverRepository,
                              ITransformer<string, string> transformer,
                              IFileSystem fileSystem,
                              IMapper mapper,
                              BaseTransformerSettings settings,
                              StorageSettings storageSettings) : base(settings, fileSystem)
        {
            Guard.AssertNotNull(editionRepository, nameof(editionRepository));
            Guard.AssertNotNull(coverRepository, nameof(coverRepository));
            Guard.AssertNotNull(transformer, nameof(transformer));
            Guard.AssertNotNull(mapper, nameof(mapper));
            Guard.AssertNotNull(storageSettings, nameof(storageSettings));

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

            TryCleanUp(imagePath);

            return new BaseResponseMessage { RawData = transformResult };
        }

        public async Task<BaseResponseMessage> UpdateEdition(EditionModel edition)
        {
            var newEdition = _mapper.Map<Edition>(edition);

            var response = new BaseResponseMessage();

            var editionToUpdate = await _editionRepository.GetEdition(newEdition.Id);

            if (editionToUpdate == null)
            {
                response.RawData = $"Edition with {newEdition.Id} wasn't found.";
                return response;
            }
            
            if (newEdition.Equals(editionToUpdate))
            {
                response.RawData = $"Edition {newEdition.Description} already existing with similar data.";
                return response;
            }

            await _editionRepository.UpdateEdition(newEdition);

            response.Id = newEdition.Id;
            return response;
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
            
            var covers = await _coverRepository.GetCoversByEdition(id);

            if (covers != null)
            {
                covers.ForEach(c => TryCleanUp(c.LinkTo));

                await _coverRepository.RemoveCovers(covers);
            }
            
            await _editionRepository.RemoveEdition(editionToRemove);
            
            response.Id = id;
            return response;
        }

        public async Task<BaseResponseMessage> CreateEdition(EditionModel edition)
        {
            var newEdtition = _mapper.Map<Edition>(edition);

            var response = new BaseResponseMessage();

            if (await _editionRepository.CheckEditionForUniqueness(newEdtition))
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
