namespace brief.Library.Entities.Profiles
{
    using System;
    using AutoMapper;
    using Controllers.Models;

    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Series, SeriesModel>();

            CreateMap<SeriesModel, Series>()
                .ForMember(d => d.Id, opt => opt.MapFrom(o => Guid.NewGuid()));
        }
    }
}
