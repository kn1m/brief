namespace brief.Controllers.Models
{
    using System;

    public class EditionModel
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? Amount { get; set; }
        public string EditionType { get; set; }
        public string Language { get; set; }
        public Guid? PublisherId { get; set; }
        public Guid? BookId { get; set; }
    }
}
