namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Series
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<Book> BooksInSeries { get; set; }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }

            var series = obj as Series;
            if (series != null)
            {
                return series.Name == Name && series.Description == Description;
            }

            return false;
        }

        public override int GetHashCode()
            => 2108858624 + EqualityComparer<Guid>.Default.GetHashCode(Id);
    }
}
