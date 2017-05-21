namespace brief.Controllers.Constraints
{
    using System;
    using System.Web;
    using System.Web.Routing;

    public class LowercaseRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var path = httpContext.Request.Url?.AbsolutePath;
            return path.Equals(path.ToLowerInvariant(), StringComparison.InvariantCulture);
        }
    }
}
