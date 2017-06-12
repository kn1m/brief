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
            => await BaseUpload(_editionService.CreateEditionFromImage, _editionService.StorageSettings);

        [HttpPost]
        public async Task<EditionModel> Create([FromBody] EditionModel edition)
            => await _editionService.CreateEdition(edition);

        [HttpPut]
        public async Task<EditionModel> Update([FromBody] EditionModel edition)
            => await _editionService.UpdateEdition(edition);

        [HttpDelete]
        public async Task Delete([FromUri] Guid id)
            => await _editionService.RemoveEdition(id);
    }
}
