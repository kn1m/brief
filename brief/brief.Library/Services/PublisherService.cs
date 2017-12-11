namespace brief.Library.Services
{
    using System;
    using System.IO.Abstractions;
    using System.Threading.Tasks;
    using AutoMapper;
    using BaseServices;
    using Controllers.Models;
    using Controllers.Models.BaseEntities;
    using Controllers.Providers;
    using Entities;
    using Helpers;
    using Repositories;

    public class PublisherService : BaseFileService, IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IEditionRepository _editionRepository;
        private readonly ICoverRepository _coverRepository;
        private readonly IMapper _mapper;

        public PublisherService(IPublisherRepository publisherRepository, 
                                IEditionRepository editionRepository,
                                ICoverRepository coverRepository,
                                IFileSystem fileSystem,
                                IMapper mapper) : base(fileSystem)
        {
            Guard.AssertNotNull(publisherRepository, nameof(publisherRepository));
            Guard.AssertNotNull(editionRepository, nameof(editionRepository));
            Guard.AssertNotNull(coverRepository, nameof(coverRepository));
            Guard.AssertNotNull(mapper, nameof(mapper));

            _coverRepository = coverRepository;
            _editionRepository = editionRepository;
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseMessage> CreatePublisher(PublisherModel publisher)
        {
            var newPublisher = _mapper.Map<Publisher>(publisher);

            var response = new BaseResponseMessage();

            if (await _publisherRepository.CheckPublisherForUniqueness(newPublisher))
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
            var newPublisher = _mapper.Map<Publisher>(publisher);

            var response = new BaseResponseMessage();

            var publisherToUpdate = await _publisherRepository.GetPublisher(newPublisher.Id);

            if (publisherToUpdate == null)
            {
                response.RawData = $"Publisher with {newPublisher.Id} wasn't found.";
                return response;
            }
            
            if (newPublisher.Equals(publisherToUpdate))
            {
                response.RawData = $"Publisher {newPublisher.Name} already existing with similar data.";
                return response;
            }

            await _publisherRepository.UpdatePublisher(newPublisher);

            response.Id = newPublisher.Id;
            return response;
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
                editionsToRemove.ForEach(async e =>
                {
                    var covers = await _coverRepository.GetCoversByEdition(e.Id);

                    if (covers != null)
                    {
                        covers.ForEach(c => TryCleanUp(c.LinkTo));

                        await _coverRepository.RemoveCovers(covers);
                    }
                });
                
                await _editionRepository.RemoveEditions(editionsToRemove);
            }
            await _publisherRepository.RemovePublisher(publisherToRemove);

            response.Id = id;
            return response;
        }
    }
}
