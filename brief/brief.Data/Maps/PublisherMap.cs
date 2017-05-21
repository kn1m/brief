namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class PublisherMap : EntityTypeConfiguration<Publisher>
    {
        public PublisherMap()
        {
            ToTable("publishers");

            HasKey(p => p.Id);

            Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(p => p.Description)
                .HasMaxLength(300);

            Property(p => p.Founded)
                .IsOptional();
        }
    }
}
