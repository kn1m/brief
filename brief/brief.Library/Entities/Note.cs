namespace brief.Library.Entities
{
    using System;

    public class Note
    {
        public Guid Id { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public int? FirstLocation { get; set; }
        public int? SecondLocation { get; set; }
        public int? Page { get; set; }
        public bool Exported { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public NoteType NoteType { get; set; }
        public Guid? EditionId { get; set; }
        public virtual Edition Edition { get; set; }
        public bool Reviewed { get; set; }
        public string Comment { get; set; }
    }
}
