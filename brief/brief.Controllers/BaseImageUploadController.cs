namespace brief.Controllers
{
    using System.IO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Helpers;
    using StreamProviders;

    public abstract class BaseImageUploadController : ApiController
    {
        protected virtual async Task<HttpResponseMessage> BaseUpload<TData>(Func<ImageModel, Task<TData>> strategy, StorageSettings storageSettings) where TData : class 
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            ImageMultipartFormDataStreamProvider provider = new ImageMultipartFormDataStreamProvider(storageSettings.StoragePath);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into ImageMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);

                //var taskList = new List<Task<TData>>();

                foreach (MultipartFileData file in provider.FileData)
                {
                    var test = file.Headers.ContentLanguage;
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                var imageToSave = new ImageModel
                {
                    Path = Path.GetFileName(provider.FileData[0].LocalFileName)
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
