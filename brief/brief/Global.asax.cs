namespace brief
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using System.Web.Http;
    using AutoMapper;
    using Library.Entities.Profiles;
    using Modules;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(BookProfile).Assembly);
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ControllersModule());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
