namespace brief.Data.Maps
{
    using System.Data.Entity.ModelConfiguration;
    using Library.Entities;

    class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("books");

            HasKey(b => b.Id);

            Property(b => b.Name)
                .HasMaxLength(100)
                .IsRequired();

            Property(b => b.Description)
                .HasMaxLength(300);

            HasOptional<Series>(b => b.Series)
                .WithMany(s => s.BooksInSeries)
                .HasForeignKey(b => b.Id);
        }
    }
}
