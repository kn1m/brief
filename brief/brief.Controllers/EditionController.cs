namespace brief.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Models;
    using Providers;

    public class EditionController : BaseImageUploadController
    {
        private readonly IEditionService _editionService;

        public EditionController(IEditionService editionService)
        {
            _editionService = editionService ?? throw new ArgumentNullException(nameof(editionService));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> RetriveData()
            => await BaseUpload(_editionService.CreateEditionFromImage);
    }
}
