namespace brief.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Providers;

    public class CoverController : BaseImageUploadController
    {
        private readonly ICoverService _coverService;

        public CoverController(ICoverService coverService)
        {
            _coverService = coverService ?? throw new ArgumentNullException(nameof(coverService));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveCover()
            => await BaseUpload(_coverService.SaveCover, _coverService.StorageSettings);

        [HttpPost]
        public async Task<HttpResponseMessage> RetrieveDataFromCover()
            => await BaseUpload(_coverService.RetrieveDataFromCover, _coverService.StorageSettings);
    }
}
