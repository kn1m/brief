namespace brief.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.UI.WebControls;
    using Models;
    using Providers;

    public class CoverController : ApiController
    {
        private readonly ICoverService _coverService;

        public CoverController(ICoverService coverService)
        {
            if (coverService == null)
            {
                throw new ArgumentNullException(nameof(coverService));
            }

            _coverService = coverService;
        }

        [ResponseType(typeof(FileUpload))]
        public IHttpActionResult UploadCover()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection  
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                if (httpPostedFile != null)
                {
                    FileUpload imgupload = new FileUpload();
                    int length = httpPostedFile.ContentLength;

                    var imageToSave = new ImageModel()
                    {
                        Length = httpPostedFile.ContentLength,
                        Data = new byte[httpPostedFile.ContentLength],
                        Name = Path.GetFileName(httpPostedFile.FileName)
                    };

                    httpPostedFile.InputStream.Read(imageToSave.Data, 0, length);

                    _coverService.SaveCover(imageToSave);

                    //imgupload.imagedata = new byte[length]; //get imagedata  
                    //httpPostedFile.InputStream.Read(imgupload.imagedata, 0, length);
                    //imgupload.imagename = Path.GetFileName(httpPostedFile.FileName);
                    //db.FileUploads.Add(imgupload);
                    //db.SaveChanges();
                    // Make sure you provide Write permissions to destination folder
                    //string sPath = @"C:\Users\xxxx\Documents\UploadedFiles";
                    //var fileSavePath = Path.Combine(sPath, httpPostedFile.FileName);
                    // Save the uploaded file to "UploadedFiles" folder  
                    //httpPostedFile.SaveAs(fileSavePath);
                    return Ok("Image Uploaded");
                }
            }
            return Ok("Image is not Uploaded");
        }
    }
}
