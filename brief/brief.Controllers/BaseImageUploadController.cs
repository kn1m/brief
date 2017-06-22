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
    using System.Web.Http;
    using Extensions;
    using Helpers;
    using Models.BaseEntities;
    using StreamProviders;

    public abstract class BaseImageUploadController : ApiController
    {
        protected virtual async Task<HttpResponseMessage> BaseUpload<TData>(Func<ImageModel, Task<TData>> strategy, 
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Specifiy lang");
            }

            //IEnumerable<string> headerValues = Request.Headers.GetValues("Target-Language");
            //var tt = headerValues.FirstOrDefault();

            //if (tt)
            //{
                
            //}

            ImageMultipartFormDataStreamProvider provider = new ImageMultipartFormDataStreamProvider(storageSettings.StoragePath);

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
                        Path = Path.GetFileName(file.LocalFileName)
                    };

                    dataTasks.Add(strategy.Invoke(imageToSave));
                }

                var results = await Task.WhenAll(dataTasks);
                var resultingDict = Enumerable.Range(0, results.Length).ToDictionary(i => files[i], i => results[i].RawData);

                return Request.CreateResponse(HttpStatusCode.OK, resultingDict);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
