namespace brief
{
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using Controllers.Constraints;
    using Controllers.Filters;
    using Controllers.Models;
    using Controllers.Models.RetrieveModels;
    using log4net.Config;
    using ExceptionLogger = Controllers.Filters.ExceptionLogger;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // log4net configuration
            XmlConfigurator.Configure();

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler());
            config.Filters.Add((ActionLogger)config.DependencyResolver.GetService(typeof(ActionLogger)));
            config.Services.Add(typeof(IExceptionLogger), config.DependencyResolver.GetService(typeof(ExceptionLogger)));

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
