namespace brief.Modules
{
    using System.Collections.Specialized;
    using System.Configuration;
    using Autofac;
    using Autofac.Core;
    using Data;
    using Library.Helpers;
    using Library.Repositories;
    using Library.Transformers;
    using Tesseract;

    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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

            builder.RegisterType<CoverRepository>()
                .As<ICoverRepository>()
                .WithParameter(new TypedParameter(typeof(string), ConfigurationManager.ConnectionStrings["briefContext"].ConnectionString));
            builder.RegisterType<SeriesRepository>()
                .As<ISeriesRepository>()
                .WithParameter(new TypedParameter(typeof(string), ConfigurationManager.ConnectionStrings["briefContext"].ConnectionString));
            builder.RegisterType<AuthorRepository>()
                .As<IAuthorRepository>()
                .WithParameter(new TypedParameter(typeof(string), ConfigurationManager.ConnectionStrings["briefContext"].ConnectionString));

            builder.RegisterType<FilterRepository>()
                .As<IFilterRepository>();
        }
    }
}