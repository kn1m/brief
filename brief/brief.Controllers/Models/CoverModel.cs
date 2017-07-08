namespace brief.Controllers.Models
{
    using System;

    public class CoverModel
    {
        public Guid Id { get; set; }
        public string LinkTo { get; set; }
        public Guid EditionId { get; set; }
    }
}
