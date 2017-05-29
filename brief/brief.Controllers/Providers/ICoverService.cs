namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models;

    public interface ICoverService
    {
        Task<CoverModel> SaveCover(ImageModel image);
        Task<CoverModel> RetrieveDataFromCover(ImageModel cover);
        Task RemoveCover(CoverModel cover);
    }
}
