namespace brief.Controllers.Providers
{
    using System;
    using System.Threading.Tasks;
    using Models;

    public interface IEditionService : IImageService
    {
        Task<EditionModel> CreateEdition(EditionModel edition);
        Task<EditionModel> CreateEditionFromImage(ImageModel image);
        Task<EditionModel> GetByIsbnFromImage(ImageModel image);
        Task<EditionModel> GetByIsbn(string isbn);
        Task<EditionModel> UpdateEdition(EditionModel edition);
        Task RemoveEdition(Guid id);
    }
}
