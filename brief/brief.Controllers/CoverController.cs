namespace brief.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Models;
    using Providers;

    public class CoverController : BaseImageUploadController
    {
        private readonly ICoverService _coverService;

        public CoverController(ICoverService coverService)
        {
            _coverService = coverService ?? throw new ArgumentNullException(nameof(coverService));
        }

        //public async Task<CoverModel> SaveCover()
        //    => await BaseUpload(_coverService.SaveCover);

        //public async Task<CoverModel> RetrieveDataFromCover()
        //    => await BaseUpload(_coverService.RetrieveDataFromCover);

    }
}
