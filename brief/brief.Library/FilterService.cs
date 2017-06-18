namespace brief.Library
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
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
            Guard.AssertNotNull(filterRepository);
            Guard.AssertNotNull(mapper);

            _filterRepository = filterRepository;
            _mapper = mapper;
        }

        public IQueryable<BookRetrieveModel> GetBooks()
            => _filterRepository.GetBooks().ProjectTo<BookRetrieveModel>(_mapper.ConfigurationProvider);
        
        public IQueryable<BookRetrieveModel> GetBookById(Guid id)
            => _filterRepository.GetBookById(id).ProjectTo<BookRetrieveModel>(_mapper.ConfigurationProvider);
    }
}
