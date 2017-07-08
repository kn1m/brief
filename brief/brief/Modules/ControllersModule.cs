namespace brief.Modules
{
    using System.Collections.Generic;
    using Autofac;
    using Autofac.Core;
    using Autofac.Integration.WebApi;
    using Controllers;
    using Controllers.Helpers;
    using Controllers.Providers;

    public class ControllersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
                                new Dictionary<string, string[]> { { "Forced", new[] { "true" }}  }}),
                    });
        }
    }
}