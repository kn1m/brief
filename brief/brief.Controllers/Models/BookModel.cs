namespace brief.Controllers.Models
{
    using System;
    using System.Collections.Generic;

    public class BookModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EditionModel> Editions { get; set; }
        public List<SeriesModel> Serieses { get; set; }
    }
}
