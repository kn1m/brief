namespace brief.Controllers.Constraints
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http.Routing;
    using System.Web.Routing;

    public class LowercaseRouteConstraint : IRouteConstraint, IHttpRouteConstraint
    {
        //TODO: remove?
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var path = httpContext.Request.Url?.AbsolutePath;
            return path.Equals(path.ToLowerInvariant(), StringComparison.InvariantCulture);
        }

        //OWIN constraint
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values,
            HttpRouteDirection routeDirection)
        {
            var path = request.RequestUri.AbsolutePath;
            return path.Equals(path.ToLowerInvariant(), StringComparison.InvariantCulture);
        }
    }
}
