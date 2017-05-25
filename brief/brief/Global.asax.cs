namespace brief
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Controllers;
    using System.Web.Http;
    using Autofac.Core;
    using AutoMapper;
    using Controllers.Providers;
    using Library;
    using Library.Repositories;
    using Data;
    using Library.Entities.Profiles;
    using Library.Helpers;
    using Library.Transformers;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var storageSettings = new StorageSettings
            {
                AllowedFormats = ConfigurationManager.AppSettings["allowedFormats"].Split(';'),
                StoragePath = ConfigurationManager.AppSettings["saveImagePath"]
            };

            var builder = new ContainerBuilder();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(BookProfile).Assembly);
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            NameValueCollection myParamsCollection =
             (NameValueCollection)ConfigurationManager.GetSection("tesseractData");

            //builder.RegisterGeneric(typeof(TesseractTransformer))
            //    .As(typeof(ITransformer<string, string>))
            //    .WithParameters(new Parameter[] { 
            //        new NamedParameter("dataPath", ConfigurationManager.GetSection("tesseractDataPath")),
            //        new NamedParameter("mode", ConfigurationManager.GetSection("tesseractDataPath"))
            //});

            builder.RegisterType<TesseractTransformer>()
                .As<ITransformer<string, string>>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("dataPath", myParamsCollection["FirstParam"]),
                    new NamedParameter("mode", myParamsCollection["FirstParam"])
                });

            builder.RegisterType<ApplicationDbContext>()
                .As<IApplicationDbContext>()
                .WithParameter(new NamedParameter("connectionString", ConfigurationManager.ConnectionStrings["briefContext"].ConnectionString));

            builder.RegisterType<BookService>()
                .As<IBookService>();

            builder.RegisterType<EditionService>()
                .As<IEditionService>()
                .WithParameter(new TypedParameter(typeof(StorageSettings), storageSettings));

            builder.RegisterType<CoverService>()
                .As<ICoverService>()
                .WithParameter(new TypedParameter(typeof(StorageSettings), storageSettings));

            builder.RegisterType<SeriesService>()
                .As<ISeriesService>();

            builder.RegisterType<BookRepository>()
                .As<IBookReporitory>();
            builder.RegisterType<EditionRepository>()
                .As<IEditionRepository>();
            builder.RegisterType<CoverRepository>()
                .As<ICoverRepository>();      
            builder.RegisterType<SeriesRepository>()
                .As<ISeriesRepository>();

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);
            builder.RegisterApiControllers(typeof(BookController).Assembly);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
