namespace brief.Library.Entities.Profiles
{
    using AutoMapper;
    using Controllers.Models;

    class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Publisher, PublisherModel>();
            CreateMap<PublisherModel, Publisher>();
        }
    }
}
