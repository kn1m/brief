namespace brief.Library.Repositories
{
    using System.Threading.Tasks;
    using Entities;

    public interface ICoverRepository
    {
        Task<bool> CheckCoverForUniqueness(Cover cover);
    }
}
