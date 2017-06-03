namespace brief.Library.Entities.Profiles
{
    using AutoMapper;
    using Controllers.Models;

    public class CoverProfile : Profile
    {
        public CoverProfile()
        {
            CreateMap<Cover, CoverModel>();
        }
    }
}
