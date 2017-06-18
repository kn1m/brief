namespace brief.Library.Entities.Profiles
{
    using AutoMapper;
    using Controllers.Models;

    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorModel>();
            CreateMap<AuthorModel, Author>();
        }
    }
}
