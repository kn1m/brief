namespace brief.Library.Entities.Profiles
{
    using System;
    using AutoMapper;
    using Controllers.Models;

    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherModel>();

            CreateMap<PublisherModel, Publisher>()
                .ForMember(d => d.Id, opt => opt.MapFrom(o => Guid.NewGuid()));
        }
    }
}
