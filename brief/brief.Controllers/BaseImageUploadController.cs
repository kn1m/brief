namespace brief.Controllers
{
    using System.IO;
    using System.Web;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using StreamProviders;

    public abstract class BaseImageUploadController : ApiController
    {
        protected BaseImageUploadController()
        {
            
        }

        protected virtual async Task<HttpResponseMessage> BaseUpload<TData>(Func<ImageModel, Task<TData>> strategy) where TData : class 
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            ImageMultipartFormDataStreamProvider provider = new ImageMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into ImageMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                var arrb = File.ReadAllBytes(provider.FileData[0].LocalFileName);

                var imageToSave = new ImageModel
                {
                    Length = arrb.Length,
                    Data = arrb,
                    Name = Path.GetFileName(provider.FileData[0].LocalFileName)
                };

                await strategy.Invoke(imageToSave);

                // Send OK Response along with saved file names to the client.
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
