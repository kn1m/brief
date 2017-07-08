namespace brief
{
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using Controllers.Constraints;
    using Controllers.Models;
    using Controllers.Models.RetrieveModels;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { url = new LowercaseRouteConstraint() }
            );

            var modelBuilder = new ODataConventionModelBuilder();

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            modelBuilder.EnableLowerCamelCase();
            modelBuilder.EntitySet<BookRetrieveModel>("books");
            modelBuilder.EntitySet<EditionModel>("editions");
            modelBuilder.EntitySet<CoverModel>("covers");

            config.MapODataServiceRoute(
                routeName: "OData",
                routePrefix: "odata",
                model: modelBuilder.GetEdmModel()
            );

            config.AddODataQueryFilter();
        }
    }
}
