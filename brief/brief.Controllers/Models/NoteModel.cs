namespace brief.Controllers.Models
{
    using System;

    public class NoteModel
    {
        public Guid Id { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public int? FirstLocation { get; set; }
        public int? SecondLocation { get; set; }
        public int? Page { get; set; }
        public DateTime? CreatedOn { get; set; }
        public NoteTypeModel NoteType { get; set; }
    }
}
