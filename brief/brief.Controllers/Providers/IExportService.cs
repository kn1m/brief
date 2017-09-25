namespace brief.Controllers.Providers
{
    using System.Threading.Tasks;

    public interface IExportService
    {
        Task<string> Export(string filePath);
    }
}