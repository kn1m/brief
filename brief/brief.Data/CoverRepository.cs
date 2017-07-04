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

        public async Task<List<Cover>> GetCoversByEdition(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Cover> GetCover(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckCoverForUniqueness(Cover cover)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveCovers(IEnumerable<Cover> covers)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCover(Cover covers)
        {
            throw new NotImplementedException();
        }
    }
}
