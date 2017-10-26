namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    public class NoteMap : EntityTypeConfiguration<Note>
    {
        public NoteMap()
        {
            ToTable("notes");

            HasKey(n => n.Id);

            Property(n => n.NoteTitle)
                .HasMaxLength(300);

            Property(n => n.NoteText)
                .HasMaxLength(4000)
                .IsRequired();

            Property(n => n.NoteType)
                .IsRequired();
        }
    }
}
