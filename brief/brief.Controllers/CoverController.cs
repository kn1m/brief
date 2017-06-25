namespace brief.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Helpers;
    using Providers;

    public class CoverController : BaseImageUploadController
    {
        private readonly ICoverService _coverService;
        private readonly IHeaderSettings _headerSettings;

        public CoverController(ICoverService coverService, IHeaderSettings headerSettings)
        {
            _coverService = coverService ?? throw new ArgumentNullException(nameof(coverService));
            _headerSettings = headerSettings ?? throw new ArgumentException(nameof(headerSettings));
        }

        [HttpPost]
        public async Task<HttpResponseMessage> SaveCover()
            => await BaseTextRecognitionUpload(_coverService.SaveCover, _coverService.StorageSettings, _headerSettings);

        [HttpPost]
        public async Task<HttpResponseMessage> RetrieveDataFromCover()
            => await BaseTextRecognitionUpload(_coverService.RetrieveDataFromCover, _coverService.StorageSettings, _headerSettings);
    }
}
