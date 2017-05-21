namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    public class EditionMap : EntityTypeConfiguration<Edition>
    {
        public EditionMap()
        {
            ToTable("editions");

            HasKey(e => e.Id);

            Property(e => e.Description)
                .HasMaxLength(300)
                .IsRequired();

            Property(e => e.Amount)
                .IsOptional();

            Property(e => e.Year)
                .IsOptional();
        }
    }
}
