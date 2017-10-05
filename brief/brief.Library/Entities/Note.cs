namespace brief.Library.Entities
{
    using System;

    public class Note
    {
        public Guid Id { get; set; }
        public string NoteTitle { get; set; }
        public string NoteText { get; set; }
        public int? Location { get; set; }
        public int? Page { get; set; }
        public DateTime CreatedOn { get; set; }
        public NoteType NoteType { get; set; }
        public virtual Edition Edition { get; set; }
    }
}
