namespace brief.Controllers.Extensions
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using Models;

    public static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessage RetrieveContentFromCover(this HttpResponseMessage response, CoverModel cover)
        {
            string mimeType = MimeMapping.GetMimeMapping(cover.LinkTo);

            StreamContent content = new StreamContent(new FileStream(cover.LinkTo, FileMode.Open));

            response.Content = content;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}
