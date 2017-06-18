namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
    using Controllers.Providers;
    using Helpers;
    using Repositories;

    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherService(IPublisherRepository publisherRepository, IMapper mapper)
        {
            Guard.AssertNotNull(publisherRepository);
            Guard.AssertNotNull(mapper);

            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreatePublisher(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> UpdatePublisher(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseMessage> RemovePublisher(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
