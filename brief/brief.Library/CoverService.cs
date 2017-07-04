namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Helpers;
    using Controllers.Models;
    using Controllers.Providers;
    using Helpers;
    using Repositories;
    using Transformers;

    public class CoverService : BaseImageService, ICoverService
    {
        public StorageSettings StorageSettings { get; }

        private readonly ICoverRepository _coverRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public CoverService(ICoverRepository coverRepository,
                            ITransformer<string, string> transformer,
                            IMapper mapper,
                            BaseTransformerSettings settings,
                            StorageSettings storageSettings) : base(settings)
        {
            Guard.AssertNotNull(coverRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);
            Guard.AssertNotNull(storageSettings);

            StorageSettings = storageSettings;

            _coverRepository = coverRepository;
            _transformer = transformer;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> SaveCover(ImageModel image)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> RetrieveDataFromCover(ImageModel cover)
        {
            var imagePath = ConvertToAppropirateFormat(cover.Path, deleteOriginal: true);

            string transformResult = await _transformer.TransformAsync(imagePath, cover.TargetLanguage);

            CleanUp(imagePath);

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

            CleanUp(coverToRemove.LinkTo);

            await _coverRepository.RemoveCover(coverToRemove);

            response.Id = id;
            return response;
        }
    }
}
