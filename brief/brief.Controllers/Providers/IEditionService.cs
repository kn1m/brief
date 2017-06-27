namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Models.BaseEntities;

    public interface IEditionService : IImageService
    {
        Task<BaseResponseMessage> CreateEdition(EditionModel edition);
        Task<BaseResponseMessage> RetrieveEditionDataFromImage(ImageModel image);
        Task<BaseResponseMessage> UpdateEdition(EditionModel edition);
        Task<BaseResponseMessage> RemoveEdition(Guid id);
    }
}
