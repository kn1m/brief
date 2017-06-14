namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface ICoverService : IImageService
    {
        Task<BaseResponseMessage> SaveCover(ImageModel image);
        Task<BaseResponseMessage> RetrieveDataFromCover(ImageModel cover);
        Task<BaseResponseMessage> RemoveCover(Guid id);
    }
}
