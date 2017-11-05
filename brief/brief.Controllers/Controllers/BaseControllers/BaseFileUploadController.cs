namespace brief.Controllers.Controllers.BaseControllers
{
    using System;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Extensions;
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

        protected virtual async Task<HttpResponseMessage> BaseUpload<TData>(Func<string, Task<TData>> strategy,
            StorageSettings storageSettings)
            where TData : BaseResponseMessage
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            
            FileMultipartFormDataStreamProvider provider = new FileMultipartFormDataStreamProvider(storageSettings.StoragePath);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var newFilename = Guid.NewGuid() + "_" + _fileSystem.Path.GetFileName(provider.FileData.First().LocalFileName);
                string newAbsolutePath = _fileSystem.Path.Combine(storageSettings.StoragePath, newFilename);

                _fileSystem.File.Move(provider.FileData.First().LocalFileName, newAbsolutePath);

                var result = await strategy.Invoke(newAbsolutePath);

                return result.CreateRespose(Request, HttpStatusCode.Created, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
