namespace brief.Library.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface ICoverRepository
    {
        Task<Guid> SaveCover(Cover cover);
        Task<List<Cover>> GetCoversByEdition(Guid id);
        Task<Cover> GetCover(Guid id);
        Task<bool> CheckCoverForUniqueness(Cover cover);
        Task RemoveCovers(IEnumerable<Cover> covers);
        Task RemoveCover(Cover cover);
    }
}
