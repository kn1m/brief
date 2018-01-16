namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            ToTable("authors");

            HasKey(a => a.Id);

            Property(a => a.AuthorFirstName)
                .HasMaxLength(100);

            Property(a => a.AuthorSecondName)
                .HasMaxLength(100);

            Property(a => a.AuthorLastName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}