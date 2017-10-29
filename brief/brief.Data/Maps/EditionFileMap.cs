namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class EditionFileMap : EntityTypeConfiguration<EditionFile>
    {
        public EditionFileMap()
        {
            ToTable("edition_files");

            HasKey(ef => ef.Id);

            Property(ef => ef.Path)
                .HasMaxLength(256)
                .IsRequired();

            Property(ef => ef.IsReaded)
                .IsRequired();

            Property(ef => ef.Type)
                .IsRequired();

            Property(ef => ef.Uploaded)
                .IsRequired();

            HasRequired<Edition>(ef => ef.Edition)
                .WithMany(e => e.Files)
                .HasForeignKey(ef => ef.EditionId);
        }
    }
}
