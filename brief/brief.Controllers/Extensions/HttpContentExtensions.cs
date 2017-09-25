namespace brief.Controllers.Extensions
{
    using System.Net.Http;

    public static class HttpContentExtensions
    {
        public static bool IsTextFilesContent(this HttpContent requestContent)
        {
            return true;
        }

        public static bool IsImageFilesContent(this HttpContent requestContent)
        {
            return true;
        }
    }
}
