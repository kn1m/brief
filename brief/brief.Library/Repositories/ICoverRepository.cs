namespace brief.Library.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface ICoverRepository
    {
        Task<List<Cover>> GetCoversByEdition(Guid id);
        Task<Edition> GetCover(Guid id);
        Task<bool> CheckCoverForUniqueness(Cover cover);
        Task RemoveCovers(IEnumerable<Cover> covers);
        Task RemoveCover(Cover covers);
    }
}
