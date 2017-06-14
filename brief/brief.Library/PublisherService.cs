namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using brief.Controllers.Models.BaseEntities;
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

        Task<BaseResponseMessage> IPublisherService.CreatePublisher(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> IPublisherService.UpdatePublisher(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponseMessage> IPublisherService.RemovePublisher(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
