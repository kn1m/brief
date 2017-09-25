namespace brief.Controllers
{
    using System;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Extensions;
    using Helpers;
    using Models;
    using Models.BaseEntities;
    using StreamProviders;

    public abstract class BaseDataFileUploadController : ApiController
    {
        private readonly IFileSystem _fileSystem;

        protected BaseDataFileUploadController() : this(new FileSystem()) {}

        protected BaseDataFileUploadController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
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
