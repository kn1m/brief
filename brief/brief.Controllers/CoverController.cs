﻿namespace brief.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Extensions;
    using Helpers.Base;
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
        public async Task<HttpResponseMessage> SaveCover([FromUri] Guid id)
            => await BaseImageUpload(_coverService.SaveCover, _coverService.StorageSettings, id);

        [HttpPost]
        public async Task<HttpResponseMessage> RetrieveDataFromCover()
            => await BaseTextRecognitionUpload(_coverService.RetrieveDataFromCover, _coverService.StorageSettings, _headerSettings);

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri] Guid id)
        {
            var result = await _coverService.RemoveCover(id);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }
    }
}
