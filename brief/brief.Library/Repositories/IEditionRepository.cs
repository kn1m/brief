namespace brief.Library.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IEditionRepository
    {
        Task<List<Edition>> GetEditionsByBook(Guid id);
        Task<Edition> GetEdition(Guid id); 
        Task<Edition> CreateEdition(Edition edition);
        Task<Edition> UpdateEdition(Edition edition);
        Task RemoveEdition(Edition edition);
        Task RemoveEditions(IEnumerable<Edition> editions);
    }
}
