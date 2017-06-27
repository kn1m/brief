namespace brief.Controllers.Models
{
    using System;
    using BaseEntities;

    public class EditionModel : IRecognizable
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
        public string EditionType { get; set; }
        public EditionTypeModel? EditionTypeModel { get; set; }
        public string Language { get; set; }
        public LanguageModel? LanguageModel { get; set; }
        public string Currency { get; set; }
        public CurrencyModel? CurrencyModel { get; set; }
        public Guid? PublisherId { get; set; }
        public Guid? BookId { get; set; }
        public string Isbn { get; set; }
        public string RawData { get; set; }
    }
}
