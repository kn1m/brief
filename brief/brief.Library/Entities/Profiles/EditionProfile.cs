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
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id ?? Guid.NewGuid()))
                .ForMember(d => d.Currency, opt => opt.MapFrom(o => o.Currency.ConvertToEnum<Currency>()))
                .ForMember(d => d.Language, opt => opt.MapFrom(o => o.Language.ConvertToEnum<Language>()))
                .ForMember(d => d.EditionType, opt => opt.MapFrom(o => o.EditionType.ConvertToEnum<EditionType>()));

            CreateMap<EditionType, EditionTypeModel>();
            CreateMap<Language, LanguageModel>();
            CreateMap<Currency, CurrencyModel>();

            CreateMap<Edition, EditionModel>()
                .ForMember(d => d.EditionType, opt => opt.Ignore())
                .ForMember(d => d.Language, opt => opt.Ignore())
                .ForMember(d => d.Currency, opt => opt.Ignore())
                .ForMember(d => d.EditionTypeModel, opt => opt.MapFrom(o => o.EditionType))
                .ForMember(d => d.LanguageModel, opt => opt.MapFrom(o => o.Language))
                .ForMember(d => d.CurrencyModel, opt => opt.MapFrom(s => (CurrencyModel?)s.Currency))
                .ForMember(d => d.RawData, opt => opt.Ignore());
        }
    }
}
