namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
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

        public Task<PublisherModel> CreatePublisher(PublisherModel publisher)
        {
            throw new System.NotImplementedException();
        }

        public Task<PublisherModel> UpdatePublisher(PublisherModel publisher)
        {
            throw new System.NotImplementedException();
        }

        public Task RemovePublisher(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
