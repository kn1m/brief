namespace brief.Controllers.Models
{
    using System;
    using System.Collections.Generic;

    public class BookModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //dirty OData hack
        //cannot be send for book creation -> to cut on validation state
        public List<EditionModel> Editions { get; set; }
        public List<SeriesModel> Serieses { get; set; }
    }
}
