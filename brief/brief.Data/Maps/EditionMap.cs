namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class EditionMap : EntityTypeConfiguration<Edition>
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

            Property(e => e.BookId)
                .IsRequired();

            Property(e => e.PublisherId)
                .IsRequired();

            Property(e => e.EditionType)
                .IsRequired();

            Property(e => e.Language)
                .IsRequired();

            Property(e => e.Isbn13)
                .HasMaxLength(13);

            Property(e => e.Isbn10)
                .HasMaxLength(10);

            HasRequired<Publisher>(e => e.Publisher)
                .WithMany(p => p.Editions)
                .HasForeignKey(e => e.PublisherId);

            HasRequired<Book>(e => e.Book)
                .WithMany(b => b.Editions)
                .HasForeignKey(e => e.BookId);
        }
    }
}
