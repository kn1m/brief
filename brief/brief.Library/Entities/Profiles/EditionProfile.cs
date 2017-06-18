namespace brief.Library.Entities.Profiles
{
    using System;
    using AutoMapper;
    using Controllers.Models;
    using Helpers;

    public class EditionProfile : Profile
    {
        public EditionProfile()
        {
            CreateMap<EditionModel, Edition>()
                .ForMember(d => d.Id, opt => opt.MapFrom(o => Guid.NewGuid()))
                .ForMember(d => d.Currency, opt => opt.MapFrom(o => o.Currency.ConvertToEnum<Currency>()))
                .ForMember(d => d.Language, opt => opt.MapFrom(o => o.Language.ConvertToEnum<Language>()))
                .ForMember(d => d.EditionType, opt => opt.MapFrom(o => o.EditionType.ConvertToEnum<EditionType>()));

            CreateMap<EditionType, EditionTypeModel>();

            CreateMap<Edition, EditionModel>()
                .ForMember(d => d.EditionType, opt => opt.Ignore())
                .ForMember(d => d.EditionTypeModel, opt => opt.MapFrom(o => o.EditionType))
                .ForMember(d => d.Language, opt => opt.Ignore())
                .ForMember(d => d.Currency, opt => opt.Ignore())
                .ForMember(d => d.RawData, opt => opt.Ignore());
        }
    }
}
