namespace brief.Library.Entities.Profiles
{
    using System;
    using AutoMapper;
    using Controllers.Models;

    public class CoverProfile : Profile
    {
        public CoverProfile()
        {
            CreateMap<Cover, CoverModel>();
            CreateMap<ImageModel, Cover>()
                .ForMember(d => d.Id, opt => opt.MapFrom(o => Guid.NewGuid()))
                .ForMember(d => d.EditionId, opt => opt.MapFrom(s => s.TargetId))
                .ForMember(d => d.LinkTo, opt => opt.MapFrom(s => s.Path));
        }
    }
}
