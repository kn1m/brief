namespace brief.Controllers.Controllers.BaseControllers
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Helpers;
    using Models.BaseEntities;
    using StreamProviders;

    public abstract class BaseFileUploadController : ApiController
    {
        private readonly IFileSystem _fileSystem;

        protected BaseFileUploadController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        protected virtual async Task<HttpResponseMessage> BaseUpload<TData>(Func<BaseUploadModel, Task<TData>> strategy,
            StorageSettings storageSettings)
            where TData : BaseResponseMessage
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
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

                    var fileToSave = new BaseUploadModel
                    {
                        Path = _fileSystem.Path.Combine(currentProviderPath, file.LocalFileName),
                    };

                    dataTasks.Add(strategy.Invoke(fileToSave));
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
    }
}
