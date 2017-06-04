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

            HasMany<Series>(b => b.Serieses)
                .WithMany(s => s.BooksInSeries)
                .Map(cs =>
                {
                    cs.MapLeftKey("BookId");
                    cs.MapRightKey("SeriesId");
                    cs.ToTable("books_in_series");
                });

            HasMany<Author>(b => b.Authors)
                .WithMany(a => a.BooksByAuthor)
                .Map(ba =>
                { 
                    ba.MapLeftKey("BookId");
                    ba.MapRightKey("AuthorId");
                    ba.ToTable("books_by_author");
                });
        }
    }
}
