namespace brief.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Extensions;
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
            => await BaseUpload(_editionService.RetrieveEditionDataFromImage, _editionService.StorageSettings);

        [HttpPost]
        public async Task<HttpResponseMessage> Create([FromBody] EditionModel edition)
        {
            var result = await _editionService.CreateEdition(edition);

            return result.CreateRespose(Request, HttpStatusCode.Created, HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> Update([FromBody] EditionModel edition)
        {
            var result = await _editionService.CreateEdition(edition);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri] Guid id)
        {
            var result = await _editionService.RemoveEdition(id);

            return result.CreateRespose(Request, HttpStatusCode.OK, HttpStatusCode.NoContent);
        }
    }
}
