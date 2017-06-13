namespace brief.Controllers.Extensions
{
    using System.Net;
    using System.Net.Http;
    using Models.BaseEntities;

    public static class BaseResponseExtensions
    {
        public static HttpResponseMessage CreateRespose(this BaseResponseMessage result, HttpRequestMessage request, HttpStatusCode success,
            HttpStatusCode failure)
        {
            if (result.RawData != null)
            {
                return request.CreateResponse(failure, result.RawData);
            }

            return request.CreateResponse(success, result.Id);
        }
    }
}
