namespace brief.Modules
{
    using Autofac;
    using Controllers.Filters;

    public class FiltersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActionLogger>()
                .AsSelf();

            builder.RegisterType<ExceptionLogger>()
                .AsSelf();
        }
    }
}