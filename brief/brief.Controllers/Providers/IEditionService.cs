namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;
    using Models;

    public interface IEditionService
    {
        Task<EditionModel> CreateEdition(EditionModel edition);
        Task<EditionModel> CreateEditionFromImage(ImageModel image);
        Task<EditionModel> UpdateEdition(EditionModel edition);
        Task RemoveEdition(EditionModel edition);
    }
}
