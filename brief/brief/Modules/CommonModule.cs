namespace brief.Modules
{
    using Autofac;
    using AutoMapper;
    using Library.Entities.Profiles;

    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(BookProfile).Assembly);
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }
    }
}