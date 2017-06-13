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
        Task<ResponseMessage<EditionModel>> RetrieveEditionObjectFromImage(ImageModel image);
        Task<BaseResponseMessage> GetByIsbnFromImage(ImageModel image);
        Task<ResponseMessage<EditionModel>> GetByIsbn(string isbn);
        Task<BaseResponseMessage> UpdateEdition(EditionModel edition);
        Task<BaseResponseMessage> RemoveEdition(Guid id);
    }
}
