namespace brief.Controllers.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Helpers.Base;

    public static class RequestExtensions
    {
        public static string RetrieveHeader(this HttpRequestMessage request,
                                                 string header,
                                                 IHeaderSettings settings)
        {
            IEnumerable<string> headerValues;

            if(request.Headers.TryGetValues(header, out headerValues))
            {
                var headersList = headerValues.ToList();

                if (headersList.Count != 1)
                {
                    return null;
                }

                var headerValue = headersList.FirstOrDefault();

                if (!settings.AcceptableValuesForHeader[header].Contains(headerValue?.ToLower()))
                {
                    return null;
                }

                return headerValue;
            }

            return null;
        }
    }
}
