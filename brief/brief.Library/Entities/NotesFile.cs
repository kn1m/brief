namespace brief.Library.Entities
{
    using System;

    public class NotesFile
    {
        public Guid Id { get; set; }
        public string Checksum { get; set; }
        public string Path { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
