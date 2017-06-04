namespace brief.Controllers.Models.RetrieveModels
{
    using System.Collections.Generic;

    public class BookRetrieveModel : BookModel
    {
        public List<EditionModel> Editions { get; set; }
        public List<SeriesModel> Serieses { get; set; }
        public List<AuthorModel> Authors { get; set; }
    }
}
