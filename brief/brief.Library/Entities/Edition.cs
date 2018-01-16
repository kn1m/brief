namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Edition
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual IList<Cover> Covers { get; set; }
        public virtual IList<Note> Notes { get; set; }
        public virtual IList<EditionFile> Files { get; set; }
        public virtual IList<Location> Locations { get; set; }
        public EditionType EditionType { get; set; }
        public Language Language { get; set; }
        public Currency? Currency { get; set; }
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }

        public override bool Equals(object obj)
        {
            var edition = obj as Edition;
            if (edition != null)
            {
                return edition.PublisherId != PublisherId ||
                       edition.Description != Description ||
                       edition.BookId != BookId ||
                       edition.EditionType != EditionType ||
                       edition.Language != Language ||
                       !edition.Year.HasValue ||
                       !Year.HasValue ||
                       edition.Year.Value == Year.Value;
            }

            return false;
        }

        public override int GetHashCode()
            => 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
    }
}
