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

        //[ResponseType(typeof(FileUpload))]
        //public async Task<IHttpActionResult> UploadCover()
        //{
        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        // Get the uploaded image from the Files collection  
        //        var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
        //        if (httpPostedFile != null)s
        //        {
        //            FileUpload imgupload = new FileUpload();
        //            int length = httpPostedFile.ContentLength;

        //            var imageToSave = new ImageModel()
        //            {
        //                Length = httpPostedFile.ContentLength,
        //                Data = new byte[httpPostedFile.ContentLength],
        //                Name = Path.GetFileName(httpPostedFile.FileName)
        //            };

        //            httpPostedFile.InputStream.Read(imageToSave.Data, 0, length);

        //            await _coverService.SaveCover(imageToSave);

        //            //imgupload.imagedata = new byte[length]; //get imagedata  
        //            //httpPostedFile.InputStream.Read(imgupload.imagedata, 0, length);
        //            //imgupload.imagename = Path.GetFileName(httpPostedFile.FileName);
        //            //db.FileUploads.Add(imgupload);
        //            //db.SaveChanges();
        //            // Make sure you provide Write permissions to destination folder
        //            //string sPath = @"C:\Users\xxxx\Documents\UploadedFiles";
        //            //var fileSavePath = Path.Combine(sPath, httpPostedFile.FileName);
        //            // Save the uploaded file to "UploadedFiles" folder  
        //            //httpPostedFile.SaveAs(fileSavePath);
        //            return Ok("Image Uploaded");
        //        }
        //    }
        //    return Ok("Image is not Uploaded");
        //}

        public async Task<CoverModel> SaveCover()
            => await BaseUpload(_coverService.SaveCover);

        public async Task<CoverModel> RetrieveDataFromCover()
            => await BaseUpload(_coverService.RetrieveDataFromCover);

    }
}
