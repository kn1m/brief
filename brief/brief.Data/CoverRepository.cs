namespace brief.Data
{
    using System.Threading.Tasks;
    using Library.Entities;
    using Library.Repositories;

    public class CoverRepository : BaseDapperRepository,  ICoverRepository
    {
        public Task<bool> CheckCoverForUniqueness(Cover cover)
        {
            throw new System.NotImplementedException();
        }
    }
}
