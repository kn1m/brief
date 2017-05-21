namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    public class SeriesMap : EntityTypeConfiguration<Series>
    {
        public SeriesMap()
        {
            ToTable("serieses");

            HasKey(s => s.Id);

            Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(s => s.Description)
                .HasMaxLength(300);
        }
    }
}
