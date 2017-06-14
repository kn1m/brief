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
    using Controllers.Helpers;
    using Controllers.Providers;
    using Library;
    using Library.Repositories;
    using Data;
    using Library.Entities.Profiles;
    using Library.Helpers;
    using Library.Transformers;
    using Tesseract;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var baseTransformerSettings = new BaseTransformerSettings
            {
                AllowedFormats = ConfigurationManager.AppSettings["allowedFormats"].Split(';')
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

            builder.RegisterType<TesseractTransformer>()
                .As<ITransformer<string, string>>()
                .WithParameters(new Parameter[]
                {
                    new NamedParameter("dataPath", myParamsCollection["TrainDataPath"]),
                    new NamedParameter("mode", myParamsCollection["EngineMode"].ConvertToEnum<EngineMode>())
                });

            builder.RegisterType<ApplicationDbContext>()
                .As<IApplicationDbContext>()
                .WithParameter(new NamedParameter("connectionString", ConfigurationManager.ConnectionStrings["briefContext"].ConnectionString));

            builder.RegisterType<BookService>()
                .As<IBookService>();

            builder.RegisterType<EditionService>()
                .As<IEditionService>()
                .WithParameters(new Parameter[]
                {
                    new TypedParameter(typeof(BaseTransformerSettings), baseTransformerSettings),
                    new TypedParameter(typeof(StorageSettings), new StorageSettings {StoragePath = ConfigurationManager.AppSettings["saveEditionPath"]})
                });

            builder.RegisterType<CoverService>()
                .As<ICoverService>()
                .WithParameters(new Parameter[]
                {
                    new TypedParameter(typeof(BaseTransformerSettings), baseTransformerSettings),
                    new TypedParameter(typeof(StorageSettings), new StorageSettings {StoragePath = ConfigurationManager.AppSettings["saveCoverPath"]})
                });

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
            builder.RegisterType<DataService>()
                .As<IDataService>();

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);
            builder.RegisterApiControllers(typeof(BookController).Assembly);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
