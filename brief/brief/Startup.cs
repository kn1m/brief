namespace brief
{
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Modules;
    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var builder = new ContainerBuilder();

            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new FiltersModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ControllersModule());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            WebApiConfig.Register(config);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}