namespace brief.Library.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IEditionRepository
    {
        Task<bool> CheckEditionForUniqueness(Edition edition);
        Task<List<Edition>> GetEditionsByBookOrPublisher(Guid id);
        Task<Edition> GetEdition(Guid id); 
        Task<Guid> CreateEdition(Edition edition);
        Task<Guid> UpdateEdition(Edition edition);
        Task RemoveEdition(Edition edition);
        Task RemoveEditions(IEnumerable<Edition> editions);
    }
}
