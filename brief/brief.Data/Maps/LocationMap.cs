namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            ToTable("locations");

            HasKey(l => l.Id);

            Property(l => l.Address)
                .HasMaxLength(100)
                .IsRequired();

            Property(l => l.Stage)
                .IsRequired();

            Property(l => l.Loker)
                .IsRequired();

            Property(l => l.Shelf)
                .IsRequired();

            HasRequired<Edition>(l => l.Edition)
                .WithMany(e => e.Locations)
                .HasForeignKey(l => l.EditionId);
        }
    }
}