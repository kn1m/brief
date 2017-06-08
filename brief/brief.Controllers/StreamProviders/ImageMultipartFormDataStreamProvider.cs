namespace brief.Controllers.StreamProviders
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class ImageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public ImageMultipartFormDataStreamProvider(string path) : base(path) {}

        public override string GetLocalFileName(HttpContentHeaders headers)
            => headers.ContentDisposition.FileName.Replace("\"", string.Empty);
    }
}
