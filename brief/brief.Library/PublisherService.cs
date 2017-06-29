namespace brief.Library
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Controllers.Models.BaseEntities;
    using Controllers.Models;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;

    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMapper _mapper;

        public PublisherService(IPublisherRepository publisherRepository, 
                                IEditionRepository editionRepository,
                                ICoverRepository coverRepository,
                                IMapper mapper)
        {
            Guard.AssertNotNull(publisherRepository);
            Guard.AssertNotNull(editionRepository);
            Guard.AssertNotNull(coverRepository);
            Guard.AssertNotNull(mapper);

            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreatePublisher(PublisherModel publisher)
        {
            var newPublisher = _mapper.Map<Publisher>(publisher);

            var response = new BaseResponseMessage();

            if (!await _publisherRepository.CheckPublisherForUniqueness(newPublisher))
            {
                response.RawData = $"Publisher {newPublisher.Name} already existing with similar data.";
                return response;
            }

            var createdPublisherId = await _publisherRepository.CreatePublisher(newPublisher);

            response.Id = createdPublisherId;
            return response;
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

            if (editionsToRemove != null)
            {
                await _editionRepository.RemoveEditions(editionsToRemove);
            }
            //remove covers
            await _publisherRepository.RemovePublisher(publisherToRemove);

            response.Id = id;
            return response;
        }
    }
}
