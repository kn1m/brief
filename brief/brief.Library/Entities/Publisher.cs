namespace brief.Library.Entities
{
    using System;
    using System.Collections.Generic;

    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Founded { get; set; }
        public IList<Book> Books { get; set; }
        public IList<Series> Series { get; set; }
    }
}
