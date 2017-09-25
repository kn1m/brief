namespace brief.Controllers
{
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
    using Helpers.Base;
    using Models.BaseEntities;
    using StreamProviders;
    using System.IO.Abstractions;

    public abstract class BaseImageUploadController : ApiController
    {
        private readonly IFileSystem _fileSystem;

        protected BaseImageUploadController() : this(new FileSystem()) {}

        protected BaseImageUploadController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

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
            string currentProviderPath = _fileSystem.Path.Combine(storageSettings.StoragePath, clientFolderId.ToString());

            _fileSystem.Directory.CreateDirectory(currentProviderPath);

            FileMultipartFormDataStreamProvider provider = new FileMultipartFormDataStreamProvider(currentProviderPath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                List<string> files = new List<string>();
                var dataTasks = new List<Task<TData>>();

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(_fileSystem.Path.GetFileName(file.LocalFileName));

                    var imageToSave = new ImageModel
                    {
                        Path = _fileSystem.Path.Combine(currentProviderPath, file.LocalFileName),
                        TargetLanguage = languageToProccess
                    };

                    dataTasks.Add(strategy.Invoke(imageToSave));
                }

                var results = await Task.WhenAll(dataTasks);

                _fileSystem.Directory.Delete(currentProviderPath);

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

            FileMultipartFormDataStreamProvider provider = new FileMultipartFormDataStreamProvider(storageSettings.StoragePath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var newFilename = Guid.NewGuid() + "_" + _fileSystem.Path.GetFileName(provider.FileData.First().LocalFileName);
                string newAbsolutePath = _fileSystem.Path.Combine(storageSettings.StoragePath, newFilename);

                _fileSystem.File.Move(provider.FileData.First().LocalFileName, newAbsolutePath);

                var imageToSave = new ImageModel
                {
                    TargetId = targetId,
                    Path = newAbsolutePath
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
