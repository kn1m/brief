namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class CoverMap : EntityTypeConfiguration<Cover>
    {
        public CoverMap()
        {
            ToTable("covers");

            HasKey(c => c.Id);

            Property(c => c.LinkTo)
                .HasMaxLength(256)
                .IsRequired();

            Property(c => c.EditionId)
                .IsRequired();

            HasRequired<Edition>(c => c.Edition)
                .WithMany(e => e.Covers)
                .HasForeignKey(c => c.EditionId);
        }
    }
}
