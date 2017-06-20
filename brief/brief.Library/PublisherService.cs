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
        private readonly IEditionRepository _editionRepository;
        private readonly IMapper _mapper;

        public PublisherService(IPublisherRepository publisherRepository, 
                                IEditionRepository editionRepository,
                                IMapper mapper)
        {
            Guard.AssertNotNull(publisherRepository);
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(mapper);

            _editionRepository = editionRepository;
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
            var response = new BaseResponseMessage();

            var publisherToRemove = await _publisherRepository.GetPublisher(id);

            if (publisherToRemove == null)
            {
                response.RawData = $"Publisher with {id} wasn't found.";

                return response;
            }

            var editionsToRemove = await _editionRepository.GetEditionsByBookOrPublisher(id);

            await _editionRepository.RemoveEditions(editionsToRemove);

            await _publisherRepository.RemovePublisher(publisherToRemove);

            response.Id = id;

            return response;
        }
    }
}
