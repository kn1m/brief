namespace brief
{
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using AutoMapper;
    using Library.Entities.Profiles;
    using Modules;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var builder = new ContainerBuilder();
            
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(BookProfile).Assembly);
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ControllersModule());

            // Set the dependency resolver to be Autofac.
            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.

            WebApiConfig.Register(config);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}