namespace brief.Library.Services
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Controllers.Models;
    using Controllers.Models.RetrieveModels;
    using Controllers.Providers;
    using Helpers;
    using Repositories;

    public class FilterService : IFilterService
    {
        private readonly IFilterRepository _filterRepository;
        private readonly IMapper _mapper;

        public FilterService(IFilterRepository filterRepository, IMapper mapper)
        {
            Guard.AssertNotNull(filterRepository, nameof(filterRepository));
            Guard.AssertNotNull(mapper, nameof(mapper));

            _filterRepository = filterRepository;
            _mapper = mapper;
        }

        public IQueryable<BookRetrieveModel> GetBooks()
            => _filterRepository.GetBooks().ProjectTo<BookRetrieveModel>(_mapper.ConfigurationProvider);
        
        public IQueryable<BookRetrieveModel> GetBookById(Guid id)
            => _filterRepository.GetBookById(id).ProjectTo<BookRetrieveModel>(_mapper.ConfigurationProvider);

        public CoverModel GetCoverById(Guid id)
            => _mapper.Map<CoverModel>(_filterRepository.GetCoverById(id));
    }
}
