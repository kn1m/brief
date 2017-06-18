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

        public Task<CoverModel> SaveCover(ImageModel image)
        {


            return null;
        }

        public Task<CoverModel> RetrieveDataFromCover(ImageModel cover)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCover(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> ICoverService.SaveCover(ImageModel image)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> ICoverService.RetrieveDataFromCover(ImageModel cover)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> ICoverService.RemoveCover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
