namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<Edition> Editions { get; set; }
        public virtual IList<Series> Serieses { get; set; }
        public virtual IList<Author> Authors { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var book = obj as Book;
            if (book != null)
            {
                return book.Name == Name
                    && book.Description == Description;
            }

            return false;
        }

        public override int GetHashCode()
            => 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
    }
}
