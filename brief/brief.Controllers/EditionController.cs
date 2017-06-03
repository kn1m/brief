namespace brief.Controllers
{
    using System;
    using System.Web.Http;
    using Providers;

    public class EditionController : ApiController
    {
        private readonly IEditionService _editionService;

        public EditionController(IEditionService editionService)
        {
            _editionService = editionService ?? throw new ArgumentNullException(nameof(editionService));
        }
    }
}
