namespace brief.Controllers
{
    using System.IO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Extensions;
    using Helpers;
    using Models.BaseEntities;
    using StreamProviders;

    public abstract class BaseImageUploadController : ApiController
    {
        protected virtual async Task<HttpResponseMessage> BaseTextRecognitionUpload<TData>(Func<ImageModel, Task<TData>> strategy, 
                                                                                           StorageSettings storageSettings,
                                                                                           IHeaderSettings headerSettings) 
            where TData : IRecognizable 
        {
            if(!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var languageToProccess = Request.RetrieveHeader("Target-Language", headerSettings);

            if (languageToProccess == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Specify appropriate target language.");
            }

            var clientFolderId = Guid.NewGuid();
            string currentProviderPath = Path.Combine(storageSettings.StoragePath, clientFolderId.ToString());

            Directory.CreateDirectory(currentProviderPath);

            ImageMultipartFormDataStreamProvider provider = new ImageMultipartFormDataStreamProvider(currentProviderPath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                List<string> files = new List<string>();
                var dataTasks = new List<Task<TData>>();

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));

                    var imageToSave = new ImageModel
                    {
                        Path = Path.Combine(currentProviderPath, file.LocalFileName),
                        TargetLanguage = languageToProccess
                    };

                    dataTasks.Add(strategy.Invoke(imageToSave));
                }

                var results = await Task.WhenAll(dataTasks);

                Directory.Delete(currentProviderPath);

                var resultingDict = Enumerable.Range(0, results.Length).ToDictionary(i => files[i], i => results[i].RawData);

                return Request.CreateResponse(HttpStatusCode.OK, resultingDict);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        protected virtual async Task<HttpResponseMessage> BaseImageUpload<TData>(Func<ImageModel, Task<TData>> strategy,
                                                                                 StorageSettings storageSettings,
                                                                                 Guid targetId)
            where TData : BaseResponseMessage
        {
            if (targetId == Guid.Empty)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Target id should be provided.");
            }

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var filesCount = HttpContext.Current.Request.Files.Count;

            if (filesCount != 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Single-file upload is only allowed. But {filesCount} files detected.");
            }

            ImageMultipartFormDataStreamProvider provider = new ImageMultipartFormDataStreamProvider(storageSettings.StoragePath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var imageToSave = new ImageModel
                {
                    Path = Path.Combine(Path.GetFileName(provider.FileData.First().LocalFileName))
                };

                var result = await strategy.Invoke(imageToSave);

                return result.CreateRespose(Request, HttpStatusCode.Created, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
