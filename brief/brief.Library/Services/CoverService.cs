namespace brief.Library.Services
{
    using System;
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

    public class CoverService : BaseTransformService, ICoverService
    {
        public StorageSettings StorageSettings { get; }

        private readonly ICoverRepository _coverRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IEditionRepository _editionRepository;
        private readonly IMapper _mapper;

        public CoverService(ICoverRepository coverRepository,
                            ITransformer<string, string> transformer,
                            IEditionRepository editionRepository,
                            IMapper mapper,
                            BaseTransformerSettings settings,
                            StorageSettings storageSettings) : base(settings)
        {
            Guard.AssertNotNull(coverRepository);
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(storageSettings);

            StorageSettings = storageSettings;

            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _transformer = transformer;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> SaveCover(ImageModel image)
        {
            var cover = _mapper.Map<Cover>(image);

            var response = new BaseResponseMessage();

            if (await _editionRepository.GetEdition(cover.EditionId) == null)
            {
                response.RawData = $"Edition with id {cover.EditionId} couldn't been found.";
                return response;
            }

            response.Id = await _coverRepository.SaveCover(cover);
            return response;
        }

        public async Task<BaseResponseMessage> RetrieveDataFromCover(ImageModel cover)
        {
            var imagePath = ConvertToAppropirateFormat(cover.Path, deleteOriginal: true);

            string transformResult = await _transformer.TransformAsync(imagePath, cover.TargetLanguage);

            TryCleanUp(imagePath);

            return new BaseResponseMessage { RawData = transformResult };
        }

        public async Task<BaseResponseMessage> RemoveCover(Guid id)
        {
            var response = new BaseResponseMessage();

            var coverToRemove = await _coverRepository.GetCover(id);

            if (coverToRemove == null)
            {
                response.RawData = $"Cover with {id} wasn't found.";
                return response;
            }

            TryCleanUp(coverToRemove.LinkTo);

            await _coverRepository.RemoveCover(coverToRemove);

            response.Id = id;
            return response;
        }
    }
}
