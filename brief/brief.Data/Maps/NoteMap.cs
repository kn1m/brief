namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class NoteMap : EntityTypeConfiguration<Note>
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

            Property(n => n.Exported)
                .IsRequired();

            Property(n => n.CreatedOn)
                .IsOptional();

            Property(n => n.ModifiedOn)
                .IsOptional();

            Property(n => n.Page)
                .IsOptional();

            Property(n => n.FirstLocation)
                .IsOptional();

            Property(n => n.SecondLocation)
                .IsOptional();

            HasOptional<Edition>(n => n.Edition)
                .WithMany(e => e.Notes)
                .HasForeignKey(n => n.EditionId);
        }
    }
}
