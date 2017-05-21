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
    }
}
