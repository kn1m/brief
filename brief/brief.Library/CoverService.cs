namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Providers;
    using Helpers;
    using Repositories;
    using Transformers;

    public class CoverService : BaseImageService, ICoverService
    {
        private readonly ICoverRepository _coverRepository;
        private readonly ITransformer<string, string> _transformer;
        private readonly IMapper _mapper;

        public CoverService(ICoverRepository coverRepository, ITransformer<string, string> transformer, IMapper mapper, StorageSettings settings) : base(settings)
        {
            Guard.AssertNotNull(coverRepository);
            Guard.AssertNotNull(transformer);
            Guard.AssertNotNull(mapper);

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

        public Task RemoveCover(BookModel book, int id)
        {
            throw new NotImplementedException();
        }
    }
}
