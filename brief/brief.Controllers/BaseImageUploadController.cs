namespace brief.Controllers
{
    using System.IO;
    using System.Web;
    using Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    public abstract class BaseImageUploadController : ApiController
    {
        protected virtual async Task<CoverModel> BaseUpload(Func<ImageModel, Task<CoverModel>> strategy)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection  
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                if (httpPostedFile != null)
                {
                    int length = httpPostedFile.ContentLength;

                    var imageToSave = new ImageModel
                    {
                        Length = httpPostedFile.ContentLength,
                        Data = new byte[httpPostedFile.ContentLength],
                        Name = Path.GetFileName(httpPostedFile.FileName)
                    };

                    httpPostedFile.InputStream.Read(imageToSave.Data, 0, length);

                    return await strategy.Invoke(imageToSave);
                }
            }
            return null;
        }
    }
}
