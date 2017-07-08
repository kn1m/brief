namespace brief.Library.Entities.Profiles
{
    using System;
    using AutoMapper;
    using Controllers.Models;
    using Controllers.Models.RetrieveModels;

    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookModel, Book>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id ?? Guid.NewGuid()));

            CreateMap<Book, BookModel>();
            CreateMap<Book, BookRetrieveModel>();
        }
    }
}
