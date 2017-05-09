namespace brief
{
    using System.Configuration;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Controllers;
    using System.Web.Http;
    using Autofac.Core;
    using Controllers.Providers;
    using Library;
    using Library.Repositories;
    using Data;
    using Library.Transformers;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            builder.RegisterGeneric(typeof(TesseractTransformer))
                .As(typeof(ITransformer<string, string>))
                .WithParameters(new Parameter[] { 
                    new NamedParameter("dataPath", ConfigurationManager.GetSection("tesseractDataPath")),
                    new NamedParameter("mode", ConfigurationManager.GetSection("tesseractDataPath"))
            });

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>();

            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<EditionService>().As<IEditionService>();
            builder.RegisterType<CoverService>().As<ICoverService>();
            builder.RegisterType<SeriesService>().As<ISeriesService>();

            builder.RegisterType<BookRepository>().As<IBookReporitory>();
            builder.RegisterType<EditionRepository>().As<IEditionRepository>();
            builder.RegisterType<CoverRepository>().As<ICoverRepository>();
            builder.RegisterType<SeriesRepository>().As<ISeriesRepository>();

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);
            builder.RegisterApiControllers(typeof(BookController).Assembly);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
