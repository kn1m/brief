namespace brief.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class CoverRepository : BaseDapperRepository,  ICoverRepository
    {
        public CoverRepository(string connectionString) : base(connectionString) {}

        public Task<List<Cover>> GetCoversByEdition(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Edition> GetCover(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckCoverForUniqueness(Cover cover)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveCovers(IEnumerable<Cover> covers)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCover(Cover covers)
        {
            throw new NotImplementedException();
        }
    }
}
