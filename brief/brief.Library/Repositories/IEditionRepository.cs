namespace brief.Library.Repositories
{
    using System.Threading.Tasks;
    using Entities;

    public interface IEditionRepository
    {
        Task<Edition> CreateEdition(Edition edition);
        Task<Edition> UpdateEdition(Edition edition);
        Task<Edition> RemoveEdition(Edition edition);
    }
}
