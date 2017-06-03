namespace brief.Library.Entities.Profiles
{
    using AutoMapper;
    using Controllers.Models;

    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Series, SeriesModel>();
            CreateMap<SeriesModel, Series>();
        }
    }
}
