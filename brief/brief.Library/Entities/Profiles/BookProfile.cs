namespace brief.Library.Entities.Profiles
{
    using System;
    using AutoMapper;
    using Controllers.Models;

    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookModel, Book>()
                .ForMember(x => x.Id, opt => opt.MapFrom(o => Guid.NewGuid()));
            //.BeforeMap((s, d) => .Id = Guid.NewGuid());
        }
    }
}
