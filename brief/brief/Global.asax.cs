namespace brief
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Drawing;
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
    using ImageFormat = System.Drawing.Imaging.ImageFormat;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var imageFormatConverter = new ImageFormatConverter();

            var baseTransformerSettings = new BaseTransformerSettings
            {
                MainTransformerFormat = (ImageFormat)imageFormatConverter.ConvertFromString(ConfigurationManager.AppSettings["mainFormat"])
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
                .WithParameter(new NamedParameter("connectionString", ConfigurationManager.ConnectionStrings["briefContext"].ConnectionString))
                .InstancePerLifetimeScope();

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
                .As<IBookRepository>();
            builder.RegisterType<EditionRepository>()
                .As<IEditionRepository>();
            builder.RegisterType<CoverRepository>()
                .As<ICoverRepository>();      
            builder.RegisterType<SeriesRepository>()
                .As<ISeriesRepository>();
            builder.RegisterType<FilterRepository>()
                .As<IFilterRepository>();
            builder.RegisterType<FilterService>()
                .As<IFilterService>();
            builder.RegisterType<HeaderSettings>()
                .As<IHeaderSettings>()
                .AsSelf();

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);
            builder.RegisterApiControllers(typeof(BookController).Assembly);

            builder.RegisterType<EditionController>()
                .WithParameters(
                    new Parameter[] {
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(IEditionService),
                            (pi, ctx) => ctx.Resolve<IEditionService>()),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "headerSettings",
                            (pi, ctx) => new HeaderSettings { AcceptableValuesForHeader =
                                new Dictionary<string, string[]> { {"Target-Language" , new[] {"ukr", "rus", "eng"}}  }}),
                    });

            builder.RegisterType<CoverController>()
                .WithParameters(
                    new Parameter[] {
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(ICoverService),
                            (pi, ctx) => ctx.Resolve<ICoverService>()),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "headerSettings",
                            (pi, ctx) => new HeaderSettings { AcceptableValuesForHeader =
                                new Dictionary<string, string[]> { {"Target-Language" , new[] {"ukr", "rus", "eng"}}  }}),
                    });

            builder.RegisterType<BookController>()
                .WithParameters(
                    new Parameter[] {
                        new ResolvedParameter(
                            (pi, ctx) => pi.ParameterType == typeof(IBookService),
                            (pi, ctx) => ctx.Resolve<IBookService>()),
                        new ResolvedParameter(
                            (pi, ctx) => pi.Name == "headerSettings",
                            (pi, ctx) => new HeaderSettings { AcceptableValuesForHeader =
                                new Dictionary<string, string[]> { { "Forced", new[] {"true"}}  }}),
                    });

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
