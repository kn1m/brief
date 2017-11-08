namespace brief.Controllers.Extensions
{
    using System.Net;
    using System.Net.Http;
    using Models.BaseEntities;

    public static class ResponseMessageExtensions
    {
        public static HttpResponseMessage CreateRespose<T>(this ResponseMessage<T> result, HttpRequestMessage request, HttpStatusCode success,
            HttpStatusCode failure)
            => result.RawData == null ? request.CreateResponse(success, result.Payload) : request.CreateResponse(failure, result.RawData);
    }
}
